window.onload = function () {
    let enableAllCheckBox = document.getElementById("enableAll");
    let enableAll = enableAllCheckBox.checked;
    let inputs = document.getElementsByClassName("check");

    if (!enableAll) {
        for (let i = 0; i < inputs.length; i++) {
            inputs[i].setAttribute('disabled', 'disabled');
        }
    }

    enableAllCheckBox.addEventListener('click', () => {
        enableAll = enableAllCheckBox.checked;
        if (!enableAll) {
            for (let i = 0; i < inputs.length; i++) {
                inputs[i].setAttribute('disabled', 'disabled');
            }
        }
        else {
            for (let i = 0; i < inputs.length; i++) {
                inputs[i].removeAttribute('disabled');
            }
        }
    });
};