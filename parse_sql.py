import re
import os

sql_file_path = '/Users/marcocarvalho/SistemasIA/AUTISTISMA/Escolas.sql'
output_path = '/Users/marcocarvalho/SistemasIA/AUTISTISMA/updates.sql'

print(f"Reading {sql_file_path}...")

try:
    with open(sql_file_path, 'r', encoding='utf-8') as f:
        content = f.read()
except Exception as e:
    print(f"Error reading file: {e}")
    exit()

print("File read. Searching for columns...")

match_columns = re.search(r"INSERT INTO \[siar_sa_sql\]\.\[Escolas\] \((.*?)\) VALUES", content)
if match_columns:
    columns = match_columns.group(1).replace('[', '').replace(']', '').split(', ')
    print(f"Columns found: {columns}")
    
    try:
        nome_index = columns.index('Nome')
        lat_index = columns.index('Latitude')
        lon_index = columns.index('Longitude')
    except ValueError:
        print("Could not find required columns")
        exit()

    inserts = content.split("INSERT INTO [siar_sa_sql].[Escolas]")
    print(f"Found {len(inserts)} potential inserts.")
    
    updates = []
    
    for insert in inserts[1:]:
        start = insert.find("VALUES (") + 8
        end = insert.rfind(")")
        if start < 8 or end == -1: continue
        
        values_str = insert[start:end]
        
        vals = []
        current_val = ""
        in_quote = False
        for char in values_str:
            if char == "'":
                in_quote = not in_quote
            
            if char == "," and not in_quote:
                vals.append(current_val.strip())
                current_val = ""
            else:
                current_val += char
        vals.append(current_val.strip())
        
        if len(vals) != len(columns):
            continue
            
        nome = vals[nome_index]
        lat = vals[lat_index]
        lon = vals[lon_index]
        
        if nome.startswith("N'"): nome = nome[2:-1]
        elif nome.startswith("'"): nome = nome[1:-1]
        
        if lat.startswith("N'"): lat = lat[2:-1]
        elif lat.startswith("'"): lat = lat[1:-1]
        
        if lon.startswith("N'"): lon = lon[2:-1]
        elif lon.startswith("'"): lon = lon[1:-1]
        
        if lat != "NULL" and lon != "NULL":
            nome_escaped = nome.replace("'", "''")
            updates.append(f"UPDATE [autistima_sa_sql].[Schools] SET [Latitude] = {lat.replace(',', '.')}, [Longitude] = {lon.replace(',', '.')} WHERE [Nome] = N'{nome_escaped}';")

    print(f"Generated {len(updates)} updates.")
    
    with open(output_path, 'w', encoding='utf-8') as f:
        f.write("\n".join(updates))
    print(f"Written to {output_path}")
else:
    print("Could not find INSERT pattern.")
