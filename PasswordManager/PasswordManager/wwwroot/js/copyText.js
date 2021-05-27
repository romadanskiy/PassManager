function copyText(id) {
    let copyElement = document.getElementById(id);
    let copyText = copyElement.value;

    let fakeArea = document.createElement('textarea');
    document.body.appendChild(fakeArea);
    fakeArea.value = copyText;
    fakeArea.select();
    document.execCommand("copy");
    document.body.removeChild(fakeArea);
    
    alert("Скопировано: " + copyText);
}