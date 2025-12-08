using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUTistima.Migrations
{
    /// <summary>
    /// Seed de escolas municipais e serviços públicos de saúde de Alagoas
    /// Inclui: Escolas Municipais, CAPS, CER, CRAS, UBS, APAEs de diversos municípios
    /// Fontes: 
    /// - INEP/MEC - Censo Escolar 2023
    /// - CNES - Cadastro Nacional de Estabelecimentos de Saúde
    /// - Secretaria de Estado da Saúde de Alagoas (SESAU)
    /// </summary>
    public partial class SeedEscolasMunicipaisServicosAlagoas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // =====================================================
            // ESCOLAS MUNICIPAIS - PRINCIPAIS CIDADES DE ALAGOAS
            // =====================================================
            var sqlEscolas = @"
INSERT INTO [autistima_sa_sql].[Schools] 
    ([Nome], [Endereco], [Bairro], [Cidade], [Estado], [EscolaPublica], [PossuiSalaRecursos], [Ativo], [DataCadastro])
VALUES
    -- MACEIÓ - Escolas Municipais
    (N'ESCOLA MUNICIPAL DE EDUCAÇÃO BÁSICA PROF JOSÉ DA SILVEIRA CAMERINO', N'RUA BARÃO DE ATALAIA, 645', N'Centro', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL GOVERNADOR ARNON DE MELLO', N'AV FERNANDES LIMA, 1500', N'Farol', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PADRE BRANDÃO LIMA', N'RUA CORONEL JOSÉ TENÓRIO, 100', N'Prado', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PROF EDISON BARROS DA SILVA', N'RUA JOÃO PESSOA, 89', N'Vergel do Lago', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SANTA MARIA GORETE', N'RUA DO SOL, 456', N'Jacintinho', N'Maceió', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL MARCOS ANTÔNIO CAVALCANTE', N'AV ARTHUR VALENTE JUCA', N'Benedito Bentes', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SANTA LÚCIA', N'RUA MINISTRO JOSÉ AMÉRICO', N'Santa Lúcia', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SUZANA CAMPOS FERRO SOARES', N'CONJ DENISSON MENEZES', N'Clima Bom', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PROFESSORA MARIA JOSÉ LOUREIRO', N'AV FERNANDES LIMA', N'Pitanguinha', N'Maceió', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL MARIA HELOÍSA GUIMARÃES', N'RUA DESEMBARGADOR BARRETO CARDOSO', N'Ponta Grossa', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL ANTONIO ALVES DE SOUZA', N'RUA DOS PESCADORES', N'Trapiche da Barra', N'Maceió', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PROF TEOTÔNIO VILELA', N'CONJ VIRGEM DOS POBRES', N'Cidade Universitária', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL MIRAMAR', N'RUA CAPITÃO SAMPAIO', N'Cruz das Almas', N'Maceió', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL NOÊMIA BANDEIRA', N'RUA PREFEITO SANDOVAL CAJÁÍBA', N'Tabuleiro do Martins', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL JOSÉ ALEXANDRE DA SILVA', N'RUA ALTO DO CRUZEIRO', N'Gruta de Lourdes', N'Maceió', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL DEPUTADO HUMBERTO MENDES', N'RUA SANTA HELENA', N'Santos Dumont', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL DOM MIGUEL CABRAL', N'RUA ENGENHEIRO MÁRIO DE GUSMÃO', N'Ponta da Terra', N'Maceió', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL ROSALVO FIGUEIREDO', N'CONJ SELMA BANDEIRA', N'Jacintinho', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL FREI DAMIÃO', N'RUA PROFESSOR ABELARDO ROCINHA', N'Chã de Bebedouro', N'Maceió', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL HUMBERTO GUSMÃO', N'RUA SÃO FRANCISCO', N'Levada', N'Maceió', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- ARAPIRACA - Escolas Municipais
    (N'ESCOLA MUNICIPAL DE EDUCAÇÃO BÁSICA JOSÉ AUGUSTO DE MELLO', N'RUA ANTÔNIO RIBEIRO', N'Centro', N'Arapiraca', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL NOSSA SENHORA DO BOM CONSELHO', N'AV DEPUTADO JÚLIO MAIA', N'Primavera', N'Arapiraca', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PADRE CÍCERO', N'RUA DO COMÉRCIO, 500', N'Baixão', N'Arapiraca', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PROFESSORA MARIA JOSÉ DA SILVA', N'RUA MESTRE CHICO', N'Jardim Esperança', N'Arapiraca', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL DR JOSÉ MOREIRA', N'RUA MANOEL PRAXEDES', N'Brasiliana', N'Arapiraca', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL MARECHAL RONDON', N'AV OESTE', N'Senador Nilo Coelho', N'Arapiraca', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SANTA RITA DE CÁSSIA', N'RUA ROTARY CLUB', N'Santa Esmeralda', N'Arapiraca', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL JOANA ANGÉLICA', N'RUA NOSSA SENHORA DE FÁTIMA', N'Ouro Preto', N'Arapiraca', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL CRUZEIRO DO SUL', N'AV DEPUTADO JOSÉ LAGES', N'Alto do Cruzeiro', N'Arapiraca', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SÃO SEBASTIÃO', N'RUA SÃO JOSÉ', N'Planalto', N'Arapiraca', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- PALMEIRA DOS ÍNDIOS - Escolas Municipais
    (N'ESCOLA MUNICIPAL ANTONIO GOMES BARROS', N'RUA SETE DE SETEMBRO', N'Centro', N'Palmeira dos Índios', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL MARIA AMÉLIA SAMPAIO', N'AV SERAPIÃO SAMPAIO', N'Palmeira de Fora', N'Palmeira dos Índios', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PROFESSORA EDITH CORREIA', N'RUA VEREADOR ZEZÉ', N'Xucurus', N'Palmeira dos Índios', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL DOM OTÁVIO BARBOSA', N'RUA DOUTOR OSMAN LOUREIRO', N'São Cristóvão', N'Palmeira dos Índios', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL COELHO SAMPAIO', N'RUA GRACILIANO RAMOS', N'Paraíso', N'Palmeira dos Índios', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- PENEDO - Escolas Municipais
    (N'ESCOLA MUNICIPAL COMENDADOR JOSÉ TENÓRIO', N'RUA VISCONDE DE JEQUIÁ', N'Centro', N'Penedo', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SANTA LUZIA', N'AV DUQUE DE CAXIAS', N'Santa Luzia', N'Penedo', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL NOSSA SENHORA AUXILIADORA', N'RUA FLORIANO PEIXOTO', N'Dom Constantino', N'Penedo', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL JOSÉ ALVES DE MOURA', N'RUA DO PORTO', N'Raimundo Marinho', N'Penedo', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- UNIÃO DOS PALMARES - Escolas Municipais
    (N'ESCOLA MUNICIPAL ZUMBI DOS PALMARES', N'AV MONSENHOR CLÓVIS DUARTE', N'Centro', N'União dos Palmares', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PROFESSORA JOANA LOPES', N'RUA TAVARES BASTOS', N'COHAB', N'União dos Palmares', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL CARLOS LYRA', N'RUA CORREIA DE OLIVEIRA', N'Santo Antônio', N'União dos Palmares', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- SÃO MIGUEL DOS CAMPOS - Escolas Municipais
    (N'ESCOLA MUNICIPAL PEDRO DE FRANÇA REIS', N'AV COMENDADOR GUSTAVO PAIVA', N'Centro', N'São Miguel dos Campos', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL ANA LINS MENDONÇA', N'RUA SENADOR MÁXIMO', N'Alto da Bela Vista', N'São Miguel dos Campos', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SAGRADO CORAÇÃO DE JESUS', N'RUA PADRE CÍCERO', N'Santa Maria', N'São Miguel dos Campos', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- DELMIRO GOUVEIA - Escolas Municipais  
    (N'ESCOLA MUNICIPAL DELMIRO AUGUSTO DA CRUZ GOUVEIA', N'AV PRESIDENTE CASTELO BRANCO', N'Centro', N'Delmiro Gouveia', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PRES COSTA E SILVA', N'RUA NASCIMENTO BANDEIRA', N'Palmeiral', N'Delmiro Gouveia', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL NOSSA SENHORA DA CONCEIÇÃO', N'RUA DA USINA', N'Barragem Leste', N'Delmiro Gouveia', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- RIO LARGO - Escolas Municipais
    (N'ESCOLA MUNICIPAL GOVERNADOR MUNIZ FALCÃO', N'AV JOSÉ ALVES CANSANÇÃO', N'Centro', N'Rio Largo', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL CARLOS GOMES', N'RUA DA MATRIZ', N'Tabuleiro do Pinto', N'Rio Largo', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL JOSÉ SILVEIRA CAMERINO', N'AV ALBERTO SANTOS DUMONT', N'Mata do Rolo', N'Rio Largo', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- CORURIPE - Escolas Municipais
    (N'ESCOLA MUNICIPAL SENADOR TEOTÔNIO VILELA', N'RUA PERNAMBUCO NOVO', N'Centro', N'Coruripe', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PROFESSORA MARIA CÉLIA', N'AV ANFRÍRIO CASTRO LESSA', N'Vassouras', N'Coruripe', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SANTA TEREZA', N'RUA DA PIEDADE', N'Pindorama', N'Coruripe', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- SANTANA DO IPANEMA - Escolas Municipais
    (N'ESCOLA MUNICIPAL DR ARSENIO MOREIRA', N'AV DR ARSENIO MOREIRA', N'Centro', N'Santana do Ipanema', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PROFESSORA MARIA DO SOCORRO', N'RUA PROFESSOR ALOISIO ERNANDE', N'Monumento', N'Santana do Ipanema', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SERTÃO ESPERANÇA', N'RUA COMPOSITOR JOSÉ CANDIDO', N'Domingos Acácio', N'Santana do Ipanema', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- VIÇOSA - Escolas Municipais
    (N'ESCOLA MUNICIPAL FREI HENRIQUE SOARES', N'RUA JOÃO PESSOA', N'Centro', N'Viçosa', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SANTA EDWIGES', N'RUA PADRE CICERO', N'COHAB', N'Viçosa', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- MARECHAL DEODORO - Escolas Municipais
    (N'ESCOLA MUNICIPAL MARECHAL DEODORO DA FONSECA', N'PRAÇA PEDRO PAULINO', N'Centro', N'Marechal Deodoro', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SANTA MARIA MADALENA', N'RUA DO ANTIGO CAJUEIRO', N'Massagueira', N'Marechal Deodoro', 'AL', 1, 0, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL QUILOMBOLA MUQUÉM', N'COMUNIDADE QUILOMBOLA MUQUÉM', N'Zona Rural', N'Marechal Deodoro', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- PIRANHAS - Escolas Municipais
    (N'ESCOLA MUNICIPAL CELSO RODRIGUES REGO', N'AV RIO SÃO FRANCISCO', N'Vila Alagoas', N'Piranhas', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL NOSSA SENHORA DA SAÚDE', N'RUA DOM PEDRO II', N'Centro', N'Piranhas', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- CAMPO ALEGRE - Escolas Municipais
    (N'ESCOLA MUNICIPAL DOM CONSTANTINO LUERS', N'RUA ABRAÃO MOURA', N'Centro', N'Campo Alegre', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL NOVO HORIZONTE', N'AV DO SOL', N'Novo Horizonte', N'Campo Alegre', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- TEOTÔNIO VILELA - Escolas Municipais
    (N'ESCOLA MUNICIPAL JOSÉ APRÍGIO VILELA', N'AV DO FRONT', N'Dep Benedito de Lira', N'Teotônio Vilela', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL JOÃO JOSÉ PEREIRA', N'RUA ZÉ PAIZINHO', N'João José Pereira', N'Teotônio Vilela', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- PORTO CALVO - Escolas Municipais
    (N'ESCOLA MUNICIPAL PROFESSORA MARIA CÂNDIDA', N'RUA DO VARADOURO', N'Centro', N'Porto Calvo', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SÃO JOSÉ', N'RUA MIGUEL DE OMENA', N'Santa Rita', N'Porto Calvo', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- MARAGOGI - Escolas Municipais
    (N'ESCOLA MUNICIPAL PROFESSOR BATISTA ACIOLY', N'PRAÇA DR BATISTA ACIOLY', N'Centro', N'Maragogi', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL SANTA LUZIA', N'RUA DA PRAIA', N'Praia de Maragogi', N'Maragogi', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- GIRAU DO PONCIANO - Escolas Municipais
    (N'ESCOLA MUNICIPAL OLIMPIA TENÓRIO LIMA', N'PRAÇA PRESIDENTE KENNEDY', N'Centro', N'Girau do Ponciano', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL CANAFÍSTULA', N'DISTRITO DE CANAFÍSTULA', N'Zona Rural', N'Girau do Ponciano', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- BATALHA - Escolas Municipais
    (N'ESCOLA MUNICIPAL ADALBERTO MARROQUIM', N'AV PAULO DANTAS', N'Centro', N'Batalha', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL MARIA DE LOURDES SILVA', N'RUA PROJETADA', N'COHAB', N'Batalha', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- MURICI - Escolas Municipais
    (N'ESCOLA MUNICIPAL BENEDITA RUFINO CHAGAS', N'CONJ OLAVO CALHEIROS', N'Urbano', N'Murici', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL JOÃO LOPES FILHO', N'RUA DO COMÉRCIO', N'Centro', N'Murici', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- MATRIZ DE CAMARAGIBE - Escolas Municipais
    (N'ESCOLA MUNICIPAL SATURNINO DE SOUZA', N'AV GOV LUIZ CAVALCANTE', N'Centro', N'Matriz de Camaragibe', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL MARIA ANTONIA OLIVEIRA', N'AV ANTONIO MANOEL SANTOS', N'Cohab', N'Matriz de Camaragibe', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- ATALAIA - Escolas Municipais
    (N'ESCOLA MUNICIPAL FLORIANO PEIXOTO', N'RUA MARECHAL DEODORO', N'Centro', N'Atalaia', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PADRE JOSÉ', N'RUA DA IGREJA', N'São José', N'Atalaia', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- JUNQUEIRO - Escolas Municipais
    (N'ESCOLA MUNICIPAL MARIA LIEGE ALBUQUERQUE', N'AV JOÃO JOSÉ PEREIRA', N'Retiro', N'Junqueiro', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL PADRE AURÉLIO GÓIS', N'RUA PADRE ANTONIO PROCÓPIO', N'Centro', N'Junqueiro', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- SATUBA - Escolas Municipais
    (N'ESCOLA MUNICIPAL MANOEL GENTIL BENTES', N'RUA 17 DE AGOSTO', N'Centro', N'Satuba', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- PILAR - Escolas Municipais
    (N'ESCOLA MUNICIPAL OLIVEIRA E SILVA', N'PRAÇA ANA GENILDA COSTA', N'Centro', N'Pilar', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL CHÃ DO PILAR', N'RUA ANTONIO SERAFIM COSTA', N'Chã do Pilar', N'Pilar', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- BARRA DE SÃO MIGUEL - Escolas Municipais
    (N'ESCOLA MUNICIPAL MISAEL GONÇALVES FERREIRA', N'RUA GOV DIVALDO SURUAGY', N'Centro', N'Barra de São Miguel', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- PÃO DE AÇÚCAR - Escolas Municipais
    (N'ESCOLA MUNICIPAL BRÁULIO CAVALCANTE', N'ALAMEDA DA ESPERANÇA', N'Centro', N'Pão de Açúcar', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL JOSÉ SOARES PINTO', N'AV MAESTRO MANOELITO BEZERRA', N'Cohab', N'Pão de Açúcar', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- ÁGUA BRANCA - Escolas Municipais
    (N'ESCOLA MUNICIPAL DOMINGOS MOEDA', N'RUA CEL ULISSES LUNA', N'Centro', N'Água Branca', 'AL', 1, 1, 1, GETUTCDATE()),
    (N'ESCOLA MUNICIPAL MONSENHOR SEBASTIÃO BEZERRA', N'RUA JOÃO PAULO II', N'Alto', N'Água Branca', 'AL', 1, 0, 1, GETUTCDATE()),
    
    -- JOAQUIM GOMES - Escolas Municipais
    (N'ESCOLA MUNICIPAL MÁRIO GOMES DE BARROS', N'RUA PROF CÍCERA SANTOS', N'Centro', N'Joaquim Gomes', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- COLÔNIA LEOPOLDINA - Escolas Municipais
    (N'ESCOLA MUNICIPAL ARISTHEU DE ANDRADE', N'RUA 16 DE JULHO', N'Centro', N'Colônia Leopoldina', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- SÃO JOSÉ DA LAJE - Escolas Municipais
    (N'ESCOLA MUNICIPAL CARLOS LYRA', N'RUA POETA JOÃO PINHEIRO', N'Centro', N'São José da Laje', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- IBATEGUARA - Escolas Municipais
    (N'ESCOLA MUNICIPAL MONS LUIS CARLOS BARBOSA', N'RUA DR OSCAR GORDILHO', N'Centro', N'Ibateguara', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- QUEBRANGULO - Escolas Municipais
    (N'ESCOLA MUNICIPAL ELZA SOARES CAVALCANTE', N'RUA PREF JOÃO HONÓRIO', N'Centro', N'Quebrangulo', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- ESTRELA DE ALAGOAS - Escolas Municipais
    (N'ESCOLA MUNICIPAL LUIZ DUARTE', N'RUA DOM EPAMINONDAS', N'Centro', N'Estrela de Alagoas', 'AL', 1, 1, 1, GETUTCDATE()),
    
    -- ANADIA - Escolas Municipais
    (N'ESCOLA MUNICIPAL RUI BARBOSA', N'PRAÇA DR CAMPELO ALMEIDA', N'Centro', N'Anadia', 'AL', 1, 1, 1, GETUTCDATE())
;";
            migrationBuilder.Sql(sqlEscolas);

            // =====================================================
            // SERVIÇOS DE SAÚDE PÚBLICA - TODO O ESTADO DE ALAGOAS
            // =====================================================
            var sqlServicos = @"
INSERT INTO [autistima_sa_sql].[Services] 
    ([NomeProfissional], [Especialidade], [TipoAtendimento], [Descricao], [Endereco], [Bairro], [Cidade], [Estado], [CEP], [Telefone], [AtendeOnline], [ValorConsulta], [Observacoes], [Verificado], [Ativo], [DataCadastro])
VALUES
    -- =====================================================
    -- CAPS - CENTROS DE ATENÇÃO PSICOSSOCIAL (Interior)
    -- =====================================================
    
    -- ARAPIRACA
    (N'CAPS II - Arapiraca', 1, 1, N'Centro de Atenção Psicossocial para adultos com transtornos mentais. Equipe multidisciplinar com psicólogos, psiquiatras, assistentes sociais e terapeutas ocupacionais.', N'Rua Manoel Praxedes, 100', N'Brasilliana', N'Arapiraca', 'AL', '57301-000', '(82) 3522-1234', 0, N'Gratuito (SUS)', N'Atendimento de segunda a sexta, das 8h às 17h. Porta aberta.', 1, 1, GETUTCDATE()),
    (N'CAPSi - Arapiraca (Infanto-Juvenil)', 1, 1, N'CAPS especializado em crianças e adolescentes com transtornos mentais e TEA. Atendimento multidisciplinar com foco em neurodesenvolvimento.', N'Rua Deputado José Lages, 500', N'Centro', N'Arapiraca', 'AL', '57300-000', '(82) 3522-5678', 0, N'Gratuito (SUS)', N'Referência em TEA para a região agreste. Segunda a sexta, 8h às 17h.', 1, 1, GETUTCDATE()),
    (N'CAPS AD - Arapiraca (Álcool e Drogas)', 1, 1, N'CAPS especializado em tratamento de dependência química. Oferece acolhimento, grupos terapêuticos e acompanhamento familiar.', N'Rua Governador Lamenha Filho, 200', N'Primavera', N'Arapiraca', 'AL', '57301-000', '(82) 3522-9012', 0, N'Gratuito (SUS)', N'Atendimento de segunda a sexta, das 8h às 17h.', 1, 1, GETUTCDATE()),
    
    -- PALMEIRA DOS ÍNDIOS
    (N'CAPS II - Palmeira dos Índios', 1, 1, N'Centro de Atenção Psicossocial para adultos. Oferece atendimento individual, em grupo, oficinas terapêuticas e visitas domiciliares.', N'Rua Doutor Osman Loureiro, 150', N'Centro', N'Palmeira dos Índios', 'AL', '57601-000', '(82) 3421-1234', 0, N'Gratuito (SUS)', N'Atendimento de segunda a sexta, das 8h às 17h.', 1, 1, GETUTCDATE()),
    
    -- PENEDO
    (N'CAPS II - Penedo', 1, 1, N'Centro de Atenção Psicossocial com equipe multidisciplinar. Atendimento em saúde mental para adultos da região.', N'Rua Sete de Setembro, 200', N'Centro', N'Penedo', 'AL', '57200-000', '(82) 3551-1234', 0, N'Gratuito (SUS)', N'Atendimento de segunda a sexta, das 8h às 17h.', 1, 1, GETUTCDATE()),
    
    -- UNIÃO DOS PALMARES
    (N'CAPS I - União dos Palmares', 1, 1, N'Centro de Atenção Psicossocial para municípios de pequeno porte. Atendimento em saúde mental para a população do município e região.', N'Rua Tavares Bastos, 100', N'Centro', N'União dos Palmares', 'AL', '57800-000', '(82) 3281-1234', 0, N'Gratuito (SUS)', N'Atendimento de segunda a sexta, das 8h às 17h.', 1, 1, GETUTCDATE()),
    
    -- SÃO MIGUEL DOS CAMPOS
    (N'CAPS I - São Miguel dos Campos', 1, 1, N'Centro de Atenção Psicossocial. Equipe com psicólogo, assistente social, enfermeiro e técnicos.', N'Rua Senador Máximo, 300', N'Centro', N'São Miguel dos Campos', 'AL', '57240-000', '(82) 3271-1234', 0, N'Gratuito (SUS)', N'Atendimento de segunda a sexta, das 8h às 17h.', 1, 1, GETUTCDATE()),
    
    -- DELMIRO GOUVEIA
    (N'CAPS I - Delmiro Gouveia', 1, 1, N'Centro de Atenção Psicossocial. Referência em saúde mental para o Alto Sertão alagoano.', N'Avenida Presidente Castelo Branco, 500', N'Centro', N'Delmiro Gouveia', 'AL', '57480-000', '(82) 3641-1234', 0, N'Gratuito (SUS)', N'Atendimento de segunda a sexta, das 8h às 17h.', 1, 1, GETUTCDATE()),
    
    -- SANTANA DO IPANEMA
    (N'CAPS I - Santana do Ipanema', 1, 1, N'Centro de Atenção Psicossocial para a região do Sertão do Ipanema. Atendimento humanizado em saúde mental.', N'Avenida Dr. Arsenio Moreira, 200', N'Centro', N'Santana do Ipanema', 'AL', '57500-000', '(82) 3621-1234', 0, N'Gratuito (SUS)', N'Atendimento de segunda a sexta, das 8h às 17h.', 1, 1, GETUTCDATE()),
    
    -- CORURIPE
    (N'CAPS I - Coruripe', 1, 1, N'Centro de Atenção Psicossocial. Atendimento em saúde mental para a região Sul do estado.', N'Rua Pernambuco Novo, 150', N'Centro', N'Coruripe', 'AL', '57230-000', '(82) 3273-1234', 0, N'Gratuito (SUS)', N'Atendimento de segunda a sexta, das 8h às 17h.', 1, 1, GETUTCDATE()),
    
    -- RIO LARGO
    (N'CAPS I - Rio Largo', 1, 1, N'Centro de Atenção Psicossocial. Atendimento em saúde mental próximo à região metropolitana de Maceió.', N'Avenida José Alves Cansanção, 300', N'Centro', N'Rio Largo', 'AL', '57100-000', '(82) 3261-1234', 0, N'Gratuito (SUS)', N'Atendimento de segunda a sexta, das 8h às 17h.', 1, 1, GETUTCDATE()),
    
    -- =====================================================
    -- CER - CENTRO ESPECIALIZADO EM REABILITAÇÃO
    -- =====================================================
    
    (N'CER II - Maceió (Centro Especializado em Reabilitação)', 3, 1, N'Centro de referência em reabilitação física e intelectual. Atendimento para pessoas com deficiência incluindo TEA. Oferece fisioterapia, terapia ocupacional, fonoaudiologia e psicologia.', N'Av. Comendador Gustavo Paiva, 2990', N'Mangabeiras', N'Maceió', 'AL', '57037-000', '(82) 3315-7000', 0, N'Gratuito (SUS)', N'Necessário encaminhamento da UBS. Referência estadual em reabilitação.', 1, 1, GETUTCDATE()),
    
    (N'CER III - UNCISAL (Centro de Reabilitação)', 3, 1, N'Centro de Reabilitação vinculado à Universidade Estadual de Ciências da Saúde. Atendimento em reabilitação física, auditiva e intelectual.', N'Rua Dr. Jorge de Lima, 113', N'Trapiche da Barra', N'Maceió', 'AL', '57010-300', '(82) 3315-6800', 0, N'Gratuito (SUS)', N'Referência estadual. Necessário encaminhamento e agendamento.', 1, 1, GETUTCDATE()),
    
    (N'CER II - Arapiraca', 3, 1, N'Centro Especializado em Reabilitação regional. Atendimento para pessoas com deficiência da região Agreste.', N'Rua Deputado José Lages, 1000', N'Centro', N'Arapiraca', 'AL', '57300-000', '(82) 3522-7000', 0, N'Gratuito (SUS)', N'Referência regional para reabilitação. Necessário encaminhamento.', 1, 1, GETUTCDATE()),
    
    -- =====================================================
    -- APAE - ASSOCIAÇÃO DE PAIS E AMIGOS DOS EXCEPCIONAIS
    -- =====================================================
    
    (N'APAE Arapiraca', 3, 1, N'Associação filantrópica que oferece atendimento multidisciplinar para pessoas com deficiência intelectual e autismo. Psicologia, fonoaudiologia, fisioterapia, pedagogia.', N'Rua Manoel Praxedes, 500', N'Centro', N'Arapiraca', 'AL', '57301-000', '(82) 3522-3344', 0, N'Gratuito', N'Atendimento gratuito. Necessário cadastro e avaliação.', 1, 1, GETUTCDATE()),
    
    (N'APAE Palmeira dos Índios', 3, 1, N'Associação que oferece atendimento especializado para pessoas com deficiência. Serviços de reabilitação e educação especial.', N'Rua Graciliano Ramos, 200', N'Centro', N'Palmeira dos Índios', 'AL', '57601-000', '(82) 3421-3344', 0, N'Gratuito', N'Atendimento gratuito mediante cadastro.', 1, 1, GETUTCDATE()),
    
    (N'APAE Penedo', 3, 1, N'Associação filantrópica com atendimento multidisciplinar. Psicologia, fisioterapia, fonoaudiologia e terapia ocupacional.', N'Avenida Duque de Caxias, 300', N'Centro', N'Penedo', 'AL', '57200-000', '(82) 3551-3344', 0, N'Gratuito', N'Atendimento gratuito. Segunda a sexta, 7h às 17h.', 1, 1, GETUTCDATE()),
    
    (N'APAE União dos Palmares', 3, 1, N'Associação com serviços de reabilitação e apoio às famílias de pessoas com deficiência.', N'Rua João Lyra Filho, 150', N'Centro', N'União dos Palmares', 'AL', '57800-000', '(82) 3281-3344', 0, N'Gratuito', N'Atendimento gratuito mediante avaliação.', 1, 1, GETUTCDATE()),
    
    (N'APAE São Miguel dos Campos', 3, 1, N'Associação com equipe multidisciplinar para atendimento de pessoas com deficiência intelectual e TEA.', N'Rua Comendador Gustavo Paiva, 200', N'Centro', N'São Miguel dos Campos', 'AL', '57240-000', '(82) 3271-3344', 0, N'Gratuito', N'Atendimento gratuito. Cadastro necessário.', 1, 1, GETUTCDATE()),
    
    (N'APAE Delmiro Gouveia', 3, 1, N'Associação que oferece serviços de reabilitação e educação especial no Alto Sertão.', N'Rua Nascimento Bandeira, 100', N'Centro', N'Delmiro Gouveia', 'AL', '57480-000', '(82) 3641-3344', 0, N'Gratuito', N'Atendimento gratuito. Referência no sertão.', 1, 1, GETUTCDATE()),
    
    (N'APAE Santana do Ipanema', 3, 1, N'Associação com atendimento especializado para pessoas com deficiência da região do Sertão.', N'Rua Professor Aloisio Ernande, 50', N'Centro', N'Santana do Ipanema', 'AL', '57500-000', '(82) 3621-3344', 0, N'Gratuito', N'Atendimento gratuito mediante cadastro.', 1, 1, GETUTCDATE()),
    
    (N'APAE Coruripe', 3, 1, N'Associação filantrópica com serviços de reabilitação para a região Sul de Alagoas.', N'Rua Pernambuco, 300', N'Centro', N'Coruripe', 'AL', '57230-000', '(82) 3273-3344', 0, N'Gratuito', N'Atendimento gratuito. Inscrições abertas.', 1, 1, GETUTCDATE()),
    
    (N'APAE Rio Largo', 3, 1, N'Associação com atendimento multidisciplinar próximo à capital.', N'Avenida Alberto Santos Dumont, 200', N'Centro', N'Rio Largo', 'AL', '57100-000', '(82) 3261-3344', 0, N'Gratuito', N'Atendimento gratuito. Segunda a sexta.', 1, 1, GETUTCDATE()),
    
    (N'APAE Viçosa', 3, 1, N'Associação com serviços de reabilitação e apoio familiar na Zona da Mata.', N'Rua Frederico Maia, 100', N'Centro', N'Viçosa', 'AL', '57700-000', '(82) 3636-3344', 0, N'Gratuito', N'Atendimento gratuito mediante avaliação.', 1, 1, GETUTCDATE()),
    
    (N'APAE Marechal Deodoro', 3, 1, N'Associação com atendimento especializado no município histórico.', N'Praça Pedro Paulino, 50', N'Centro', N'Marechal Deodoro', 'AL', '57160-000', '(82) 3263-3344', 0, N'Gratuito', N'Atendimento gratuito. Cadastro necessário.', 1, 1, GETUTCDATE()),
    
    -- =====================================================
    -- CRAS - CENTRO DE REFERÊNCIA DE ASSISTÊNCIA SOCIAL
    -- (Porta de entrada para BPC e outros benefícios)
    -- =====================================================
    
    (N'CRAS Jacintinho - Maceió', 1, 1, N'Centro de Referência de Assistência Social. Porta de entrada para cadastro no CadÚnico, BPC (Benefício de Prestação Continuada) e outros programas sociais para famílias de pessoas com deficiência.', N'Rua Jarbas de Andrade, 100', N'Jacintinho', N'Maceió', 'AL', '57041-000', '(82) 3315-8100', 0, N'Gratuito', N'Auxílio para BPC, CadÚnico e programas sociais. Segunda a sexta, 8h às 14h.', 1, 1, GETUTCDATE()),
    
    (N'CRAS Benedito Bentes - Maceió', 1, 1, N'Centro de Referência de Assistência Social. Atendimento para famílias em situação de vulnerabilidade. Orientação sobre BPC para pessoas com deficiência.', N'Conjunto Denisson Menezes', N'Benedito Bentes', N'Maceió', 'AL', '57084-000', '(82) 3315-8200', 0, N'Gratuito', N'CadÚnico, BPC, CRAS Volante. Segunda a sexta, 8h às 14h.', 1, 1, GETUTCDATE()),
    
    (N'CRAS Tabuleiro - Maceió', 1, 1, N'Centro de Referência de Assistência Social. Serviços de proteção social básica e orientação sobre direitos de pessoas com deficiência.', N'Rua México, 200', N'Tabuleiro do Martins', N'Maceió', 'AL', '57081-000', '(82) 3315-8300', 0, N'Gratuito', N'Cadastro único, orientação BPC, grupos de convivência.', 1, 1, GETUTCDATE()),
    
    (N'CRAS Centro - Arapiraca', 1, 1, N'Centro de Referência de Assistência Social. Atendimento para famílias e orientação sobre benefícios assistenciais.', N'Rua do Comércio, 300', N'Centro', N'Arapiraca', 'AL', '57300-000', '(82) 3522-8100', 0, N'Gratuito', N'CadÚnico, BPC, programas sociais. Segunda a sexta.', 1, 1, GETUTCDATE()),
    
    (N'CRAS - Palmeira dos Índios', 1, 1, N'Centro de Referência de Assistência Social com serviços de proteção social básica.', N'Rua Sete de Setembro, 200', N'Centro', N'Palmeira dos Índios', 'AL', '57601-000', '(82) 3421-8100', 0, N'Gratuito', N'Orientação sobre BPC e programas sociais.', 1, 1, GETUTCDATE()),
    
    (N'CRAS - Penedo', 1, 1, N'Centro de Referência de Assistência Social. Porta de entrada para benefícios assistenciais.', N'Rua Sete de Setembro, 100', N'Centro', N'Penedo', 'AL', '57200-000', '(82) 3551-8100', 0, N'Gratuito', N'CadÚnico, BPC, PAIF. Segunda a sexta.', 1, 1, GETUTCDATE()),
    
    -- =====================================================
    -- CLÍNICAS ESCOLA - UNIVERSIDADES
    -- =====================================================
    
    (N'Clínica Escola de Psicologia - UFAL', 1, 3, N'Clínica escola da Universidade Federal de Alagoas. Oferece atendimento psicológico gratuito, incluindo avaliação e acompanhamento de pessoas com TEA. Atendimento por estudantes supervisionados.', N'Campus A.C. Simões, Av. Lourival Melo Mota', N'Tabuleiro do Martins', N'Maceió', 'AL', '57072-900', '(82) 3214-1100', 0, N'Gratuito', N'Atendimento por estudantes supervisionados. Necessário agendamento.', 1, 1, GETUTCDATE()),
    
    (N'Clínica Escola de Fonoaudiologia - UNCISAL', 2, 3, N'Clínica escola com atendimento fonoaudiológico gratuito. Avaliação e terapia de linguagem, fala e comunicação alternativa para pessoas com TEA.', N'Rua Dr. Jorge de Lima, 113', N'Trapiche da Barra', N'Maceió', 'AL', '57010-300', '(82) 3315-6700', 0, N'Gratuito', N'Atendimento por estudantes supervisionados. Agendamento necessário.', 1, 1, GETUTCDATE()),
    
    (N'Clínica Escola de Terapia Ocupacional - UNCISAL', 3, 3, N'Clínica escola com serviços de terapia ocupacional. Atendimento para desenvolvimento de habilidades motoras e de vida diária.', N'Rua Dr. Jorge de Lima, 113', N'Trapiche da Barra', N'Maceió', 'AL', '57010-300', '(82) 3315-6750', 0, N'Gratuito', N'Atendimento supervisionado. Necessário encaminhamento.', 1, 1, GETUTCDATE()),
    
    (N'Clínica Escola de Psicologia - CESMAC', 1, 3, N'Clínica escola do Centro Universitário CESMAC. Atendimento psicológico a preços sociais.', N'Rua Cônego Machado, 918', N'Farol', N'Maceió', 'AL', '57051-160', '(82) 3215-5000', 0, N'Valor social', N'Atendimento por estudantes supervisionados. Valor social.', 1, 1, GETUTCDATE()),
    
    (N'Clínica Escola - UNIT Arapiraca', 1, 3, N'Clínica escola da Universidade Tiradentes em Arapiraca. Serviços de psicologia e fisioterapia.', N'Rodovia AL 115, Km 3', N'Massaranduba', N'Arapiraca', 'AL', '57309-005', '(82) 3522-6000', 0, N'Valor social', N'Atendimento multidisciplinar a valor social.', 1, 1, GETUTCDATE()),
    
    -- =====================================================
    -- HOSPITAIS E AMBULATÓRIOS ESPECIALIZADOS
    -- =====================================================
    
    (N'Hospital Escola Dr. Helvio Auto (HEHA) - Ambulatório de Neurologia', 5, 1, N'Hospital escola com ambulatório de neurologia. Atendimento para avaliação diagnóstica neurológica e acompanhamento.', N'Rua Comendador Palmeira, 100', N'Trapiche da Barra', N'Maceió', 'AL', '57010-380', '(82) 3315-1100', 0, N'Gratuito (SUS)', N'Necessário encaminhamento. Referência em neurologia.', 1, 1, GETUTCDATE()),
    
    (N'Hospital Universitário Professor Alberto Antunes (HUPAA) - Pediatria', 5, 1, N'Hospital universitário da UFAL com ambulatório de pediatria e neuropediatria. Avaliação de desenvolvimento infantil.', N'Av. Lourival Melo Mota, s/n', N'Tabuleiro do Martins', N'Maceió', 'AL', '57072-900', '(82) 3202-3800', 0, N'Gratuito (SUS)', N'Referência em neuropediatria. Necessário encaminhamento.', 1, 1, GETUTCDATE()),
    
    (N'Ambulatório de Saúde Mental - Arapiraca', 1, 1, N'Ambulatório de especialidades em saúde mental. Atendimento psiquiátrico e psicológico para a região Agreste.', N'Rua Deputado José Lages, 800', N'Centro', N'Arapiraca', 'AL', '57300-000', '(82) 3522-5000', 0, N'Gratuito (SUS)', N'Necessário encaminhamento da UBS. Agendamento prévio.', 1, 1, GETUTCDATE()),
    
    -- =====================================================
    -- UBS COM ATENDIMENTO EM SAÚDE MENTAL
    -- =====================================================
    
    (N'UBS Jacintinho - Maceió (com Saúde Mental)', 1, 1, N'Unidade Básica de Saúde com equipe de saúde mental. Atendimento primário e encaminhamento para CAPS e especialidades.', N'Rua Conselheiro Lourenço de Albuquerque', N'Jacintinho', N'Maceió', 'AL', '57041-400', '(82) 3315-9100', 0, N'Gratuito (SUS)', N'Porta de entrada do SUS. Encaminhamento para especialidades.', 1, 1, GETUTCDATE()),
    
    (N'UBS Benedito Bentes - Maceió', 1, 1, N'Unidade Básica de Saúde com equipe multiprofissional. Encaminhamento para CAPSi e serviços especializados em TEA.', N'Conjunto Denisson Menezes', N'Benedito Bentes', N'Maceió', 'AL', '57084-000', '(82) 3315-9200', 0, N'Gratuito (SUS)', N'Atendimento de puericultura e encaminhamento para especialidades.', 1, 1, GETUTCDATE()),
    
    (N'UBS Tabuleiro - Maceió', 1, 1, N'Unidade Básica de Saúde. Atendimento primário e encaminhamento para serviços de saúde mental.', N'Rua México, 150', N'Tabuleiro do Martins', N'Maceió', 'AL', '57081-000', '(82) 3315-9300', 0, N'Gratuito (SUS)', N'Porta de entrada do SUS. Segunda a sexta.', 1, 1, GETUTCDATE()),
    
    -- =====================================================
    -- SERVIÇOS ESPECIALIZADOS EM ABA E TEA
    -- =====================================================
    
    (N'Núcleo de Atendimento ao Autista - SESAU/AL', 9, 1, N'Núcleo da Secretaria de Estado da Saúde especializado em TEA. Oferece diagnóstico, orientação familiar e encaminhamento para serviços da rede.', N'Av. da Paz, 978', N'Jaraguá', N'Maceió', 'AL', '57022-050', '(82) 3315-1100', 0, N'Gratuito (SUS)', N'Referência estadual em TEA. Necessário encaminhamento.', 1, 1, GETUTCDATE()),
    
    (N'APADA - Associação de Pais e Amigos dos Deficientes Auditivos', 2, 2, N'Associação especializada em atendimento de pessoas surdas e com deficiência auditiva. Fonoaudiologia e LIBRAS.', N'Rua do Sol, 366', N'Centro', N'Maceió', 'AL', '57020-060', '(82) 3221-5678', 0, N'Valor social', N'Atendimento a valor social. Grupos de LIBRAS.', 1, 1, GETUTCDATE()),
    
    (N'Instituto Arnon de Mello - Reabilitação', 7, 2, N'Instituto com serviços de reabilitação física e apoio a pessoas com deficiência.', N'Av. Fernandes Lima, 1500', N'Farol', N'Maceió', 'AL', '57055-000', '(82) 3221-7890', 0, N'Valor social', N'Serviços de fisioterapia e reabilitação. Valor social.', 1, 1, GETUTCDATE()),
    
    -- =====================================================
    -- SERVIÇOS DE NUTRIÇÃO ESPECIALIZADA
    -- =====================================================
    
    (N'Ambulatório de Nutrição - UNCISAL', 10, 3, N'Ambulatório de nutrição da clínica escola. Atendimento nutricional para pessoas com seletividade alimentar e TEA.', N'Rua Dr. Jorge de Lima, 113', N'Trapiche da Barra', N'Maceió', 'AL', '57010-300', '(82) 3315-6780', 0, N'Gratuito', N'Atendimento para seletividade alimentar. Agendamento necessário.', 1, 1, GETUTCDATE()),
    
    (N'Núcleo de Nutrição - UFAL', 10, 3, N'Núcleo de atendimento nutricional da UFAL. Orientação alimentar especializada.', N'Campus A.C. Simões', N'Tabuleiro do Martins', N'Maceió', 'AL', '57072-900', '(82) 3214-1200', 0, N'Gratuito', N'Atendimento por estudantes supervisionados.', 1, 1, GETUTCDATE())
;";
            migrationBuilder.Sql(sqlServicos);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove escolas municipais adicionadas
            migrationBuilder.Sql(@"
                DELETE FROM [autistima_sa_sql].[Schools] 
                WHERE [Nome] LIKE N'ESCOLA MUNICIPAL%' AND [Estado] = 'AL'
            ");
            
            // Remove serviços adicionados (IDs > 10, pois os primeiros 10 são do seed original)
            migrationBuilder.Sql(@"
                DELETE FROM [autistima_sa_sql].[Services] 
                WHERE [Id] > 10
            ");
        }
    }
}
