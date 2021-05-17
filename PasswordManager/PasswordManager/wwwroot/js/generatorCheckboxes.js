window.onload = function () {
    let enableAllCheckBox = document.getElementById("enableAll");
    let enableAll = enableAllCheckBox.checked;
    let inputs = document.getElementsByClassName("check");
    let checkBoxes = document.getElementsByClassName("check-box");

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
    
    let checkedCount = findChecked();
    
    function findChecked() {
        let count = 0;
        for (let i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].checked)
                count = count + 1;
        }
        return count;
    }

    for (let i = 0; i < checkBoxes.length; i++) {
        checkBoxes[i].addEventListener('click', () => {
            if (checkedCount === 1 && !checkBoxes[i].checked)
                checkBoxes[i].checked = true;
            else if (checkBoxes[i].checked)
                checkedCount = checkedCount + 1;
            else
                checkedCount = checkedCount - 1;
        });
    }
};