$(document).ready(function(){
    // Máscara para CPF
    $('.cpf-mask, input[name="CPF"], #CPF').mask('000.000.000-00', {reverse: true});
    
    // Máscara para CNPJ
    $('.cnpj-mask, input[name="CNPJ"], #CNPJ').mask('00.000.000/0000-00', {reverse: true});
    
    // Máscara para CEP
    $('.cep-mask, input[name="CEP"], #CEP').mask('00000-000');
    
    // Máscara para Telefone (com 8 ou 9 dígitos)
    var behavior = function (val) {
        return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
    },
    options = {
        onKeyPress: function (val, e, field, options) {
            field.mask(behavior.apply({}, arguments), options);
        }
    };
    
    $('.phone-mask, input[name="PhoneNumber"], #PhoneNumber, input[name="Telefone"], #Telefone, input[type="tel"]').mask(behavior, options);
    
    // Máscara para Data
    $('.date-mask').mask('00/00/0000');
    
    // Máscara para Dinheiro
    $('.money-mask').mask('#.##0,00', {reverse: true});
});
