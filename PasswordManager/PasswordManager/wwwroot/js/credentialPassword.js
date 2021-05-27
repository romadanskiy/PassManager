window.onload = function () {
    let divs = document.getElementsByClassName('credential-password-click');
    for (let i = 0; i < divs.length; i++) {
        let input = divs[i].firstElementChild;
        divs[i].addEventListener('click', () => {
            if (input.getAttribute('type') === 'text')
                input.setAttribute('type', 'password');
            else
                input.setAttribute('type', 'text');
        })
    }
}