#!/bin/bash

# Script para liberar porta e executar o projeto AUTistima
# Uso: ./testar.sh [porta]

PORTA=${1:-5000}

echo "🔍 Verificando processos na porta $PORTA..."

# Encontra e mata processos usando a porta
PID=$(lsof -ti:$PORTA)

if [ -n "$PID" ]; then
    echo "⚠️  Processo encontrado na porta $PORTA (PID: $PID)"
    echo "🔄 Finalizando processo..."
    kill -9 $PID 2>/dev/null
    sleep 1
    echo "✅ Porta $PORTA liberada!"
else
    echo "✅ Porta $PORTA já está livre"
fi

echo ""
echo "🚀 Iniciando AUTistima..."
echo "📍 Acesse: http://localhost:$PORTA"
echo ""

# Entra na pasta do projeto e executa com compilação completa
cd "$(dirname "$0")/AUTistima"
dotnet run --urls "http://localhost:$PORTA"
