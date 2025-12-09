import re

def parse_value(val):
    val = val.strip()
    if val == 'NULL':
        return None
    if val.startswith("N'"):
        return val[2:-1]
    if val.startswith("'"):
        return val[1:-1]
    return val

def generate_sql():
    input_file = 'Escolas.sql'
    output_file = 'seed_schools.sql'
    
    # Regex to match INSERT statements
    # INSERT INTO [siar_sa_sql].[Escolas] ([Id], [Inep], [Nome], ..., [Latitude], [Longitude]) VALUES (N'238', ..., N'-9.6365709', N'-35.7110774')
    insert_pattern = re.compile(r"INSERT INTO \[siar_sa_sql\]\.\[Escolas\] \((.*?)\) VALUES \((.*?)\)")
    
    with open(input_file, 'r', encoding='utf-8') as f:
        content = f.read()
        
    matches = insert_pattern.findall(content)
    
    sql_statements = []
    sql_statements.append("DELETE FROM [autistima_sa_sql].[Schools];")
    
    for columns_str, values_str in matches:
        columns = [c.strip().strip('[]') for c in columns_str.split(',')]
        
        # Split values respecting quotes (simple split by comma might fail if commas in strings)
        # But looking at the file, values are N'...' or NULL. 
        # Let's use a smarter split or regex for values.
        # The values seem to be separated by ", "
        
        # A simple split by ", " might work if no commas inside values. 
        # Let's try a regex for values extraction.
        values = []
        current_val = ""
        in_quote = False
        for char in values_str:
            if char == "'":
                in_quote = not in_quote
            if char == "," and not in_quote:
                values.append(current_val.strip())
                current_val = ""
            else:
                current_val += char
        values.append(current_val.strip())
        
        data = dict(zip(columns, values))
        
        # Extract fields
        nome = parse_value(data.get('Nome', 'NULL'))
        endereco = parse_value(data.get('Endereco', 'NULL'))
        bairro = parse_value(data.get('Bairro', 'NULL'))
        cep = parse_value(data.get('CEP', 'NULL'))
        latitude = parse_value(data.get('Latitude', 'NULL'))
        longitude = parse_value(data.get('Longitude', 'NULL'))
        
        # Defaults
        cidade = "Maceió"
        estado = "AL"
        
        # Try to extract city from address if possible
        if endereco and " - " in endereco:
            parts = endereco.split(" - ")
            if len(parts) > 1:
                potential_city = parts[-1].strip()
                if potential_city.upper() != "MACEIÓ":
                    # It might be the city, but let's stick to Maceió/AL as per context unless obvious
                    pass

        # Escape single quotes in strings for SQL
        def escape(s):
            if s is None: return "NULL"
            return "N'" + s.replace("'", "''") + "'"

        # Build INSERT statement
        # Target: [Nome], [CNPJ], [Endereco], [Bairro], [Cidade], [Estado], [CEP], [Telefone], [Email], [EscolaPublica], [PossuiSalaRecursos], [Latitude], [Longitude], [Ativo], [DataCadastro]
        
        val_nome = escape(nome)
        val_cnpj = "NULL"
        val_endereco = escape(endereco)
        val_bairro = escape(bairro)
        val_cidade = escape(cidade)
        val_estado = escape(estado)
        val_cep = escape(cep)
        val_telefone = "NULL"
        val_email = "NULL"
        val_publica = "1" # True
        val_sala = "0" # False
        val_lat = latitude if latitude else "NULL"
        val_lon = longitude if longitude else "NULL"
        val_ativo = "1"
        val_data = "GETDATE()"
        
        # Handle Latitude/Longitude being strings in source but numbers in target
        # The source has N'-9.63...' so parse_value removes N''. 
        # If it was NULL, it returns None.
        # We need to ensure it's a valid number or NULL.
        
        sql = f"INSERT INTO [autistima_sa_sql].[Schools] ([Nome], [CNPJ], [Endereco], [Bairro], [Cidade], [Estado], [CEP], [Telefone], [Email], [EscolaPublica], [PossuiSalaRecursos], [Latitude], [Longitude], [Ativo], [DataCadastro]) VALUES ({val_nome}, {val_cnpj}, {val_endereco}, {val_bairro}, {val_cidade}, {val_estado}, {val_cep}, {val_telefone}, {val_email}, {val_publica}, {val_sala}, {val_lat}, {val_lon}, {val_ativo}, {val_data});"
        
        sql_statements.append(sql)
        
    with open(output_file, 'w', encoding='utf-8') as f:
        f.write('\n'.join(sql_statements))
    
    print(f"Generated {len(sql_statements)} statements (including DELETE).")

if __name__ == "__main__":
    generate_sql()
