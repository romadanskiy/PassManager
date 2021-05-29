let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("Receive", function (message, user, isTechSupport) {
    
    let currentIsTechSupport = document.getElementById("isTechSupport").value.toLowerCase() === "true";
    
    let msg = message.replace(/&/g, "&").replace(/>/g, ">");
    
    let b = document.createElement("b");
    b.textContent = user;
    if (isTechSupport) {
        b.classList.add("support-message");
    }
    if (currentIsTechSupport && !isTechSupport) {
        b.classList.add("to-answer");
    }
    
    let li = document.createElement("li");
    li.append(b);
    li.append(": ")
    li.append(msg)
    
    document.getElementById("messagesList").appendChild(li);
    
    if (currentIsTechSupport) {
        let toAnswers = document.getElementsByClassName("to-answer")
        for (let i=0;i<toAnswers.length;i++) {
            toAnswers[i].addEventListener('click', () => {
                let user = toAnswers[i].textContent;

                let messageInput = document.getElementById('message');
                messageInput.value = user + ", ";

                let receiverInput = document.getElementById("receiver");
                receiverInput.value = user;
            })
        }
    }
});

document.getElementById("sendBtn").addEventListener("click", function (event) {
    let receiver = document.getElementById("receiver").value;
    let message = document.getElementById("message").value;
    if (receiver.length > 1 && message.length > 1) {
        connection.invoke("Send", message, receiver).catch(function (err) {
            return console.error(err.toString());
        });
    }

    event.preventDefault();
});