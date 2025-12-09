import os

migration_file = 'AUTistima/Migrations/20251208195820_ClearAndSeedSchools.cs'
seed_file = 'seed_schools.sql'

with open(migration_file, 'r') as f:
    content = f.read()

with open(seed_file, 'r') as f:
    sql_content = f.read()

# Construct the SQL block
sql_block = f'            migrationBuilder.Sql(@"{sql_content}");'

# Check if already at the top
if 'protected override void Up(MigrationBuilder migrationBuilder)\n        {\n            migrationBuilder.Sql' in content:
    print("SQL already at the top.")
else:
    # Remove from the end if present
    # We need to find the block starting with migrationBuilder.Sql(@"\nDELETE FROM
    # and ending with ");\n        }
    
    # This is tricky because of whitespace.
    # Let's try to split by the start marker.
    start_marker = 'migrationBuilder.Sql(@"\nDELETE FROM [autistima_sa_sql].[Schools];'
    if start_marker in content:
        parts = content.split(start_marker)
        if len(parts) > 1:
            # The last part contains the rest of the SQL and the end of the method
            # We need to find where the SQL ends.
            # It ends with ");
            end_marker = '");'
            last_part = parts[-1]
            end_index = last_part.rfind(end_marker)
            if end_index != -1:
                # Reconstruct content without the SQL block
                # We keep the part before the marker, and the part after the end marker
                # But we need to be careful about the closing brace of the method
                
                # Actually, let's just replace the whole block if we can identify it.
                # Or better, just insert at the top and let the bottom one be (it will run twice? No, delete first then insert).
                # If we run DELETE twice, it's fine.
                # If we run INSERT twice, we get primary key violations?
                # The table has an Identity column? No, the INSERTs don't specify ID?
                # Wait, the INSERTs in seed_schools.sql DO NOT specify ID.
                # So we will get duplicates if we run it twice.
                
                # So we MUST remove the old one.
                pass

# Let's try a different approach.
# We know the file structure.
# We can find the `Up` method start.
# We can find the `UpdateData` calls.
# We can find the `migrationBuilder.Sql` at the end.

lines = content.splitlines()
new_lines = []
in_sql_block = False
sql_block_removed = False

up_method_start_index = -1

for i, line in enumerate(lines):
    if 'protected override void Up(MigrationBuilder migrationBuilder)' in line:
        up_method_start_index = i
        new_lines.append(line)
        new_lines.append('        {')
        # Insert SQL block here
        new_lines.append(sql_block)
        continue
    
    if line.strip() == '{' and i == up_method_start_index + 1:
        continue # Already added

    if 'migrationBuilder.Sql(@"' in line and 'DELETE FROM' in lines[i+1]:
        in_sql_block = True
        sql_block_removed = True
        continue
    
    if in_sql_block:
        if '");' in line:
            in_sql_block = False
        continue
    
    new_lines.append(line)

if sql_block_removed:
    print("Removed SQL block from end.")
else:
    print("Did not find SQL block at end (or it was different format).")

# Write back
with open(migration_file, 'w') as f:
    f.write('\n'.join(new_lines))

print("Updated migration file.")
