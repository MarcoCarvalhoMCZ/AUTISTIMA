
/**
 * Gerenciador de Ajuda Contextual
 */
function initContextualHelp() {
    const helpContent = document.getElementById('page-help-content');
    const helpBtn = document.getElementById('btnHelpFloating');
    const modalContent = document.getElementById('helpModalContent');

    if (helpContent && helpBtn && modalContent) {
        console.log('✅ Ajuda contextual encontrada para esta página.');
        
        // Mostra o botão flutuante
        helpBtn.classList.remove('d-none');

        // Ao abrir o modal, copia o conteúdo
        const helpModal = document.getElementById('helpModal');
        if (helpModal) {
            helpModal.addEventListener('show.bs.modal', function () {
                modalContent.innerHTML = helpContent.innerHTML;
            });
        }
    } else {
        console.log('ℹ️ Sem ajuda contextual específica para esta página.');
    }
}

// Adiciona à inicialização
document.addEventListener('DOMContentLoaded', function() {
    initContextualHelp();
});
