"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceberMensagem", function (mensagem) {
    // Verifica se a mensagem é para a conversa atual
    var currentUrl = window.location.href;
    if (currentUrl.includes(mensagem.remetenteId)) {
        var div = document.createElement("div");
        div.className = "d-flex justify-content-start mb-3";
        div.innerHTML = `
            <div class="bg-light rounded p-3 shadow-sm" style="max-width: 75%;">
                <p class="mb-1 fw-bold small text-muted">${mensagem.nomeRemetente}</p>
                <p class="mb-0">${mensagem.conteudo}</p>
                <small class="text-muted d-block text-end mt-1" style="font-size: 0.7rem;">${mensagem.dataEnvio}</small>
            </div>
        `;
        document.getElementById("chat-messages").appendChild(div);
        
        // Scroll para o final
        var chatContainer = document.getElementById("chat-messages");
        chatContainer.scrollTop = chatContainer.scrollHeight;
    } else {
        // Opcional: Mostrar notificação toast
        // alert("Nova mensagem de " + mensagem.nomeRemetente);
    }
});

connection.start().then(function () {
    console.log("SignalR Connected");
}).catch(function (err) {
    return console.error(err.toString());
});
