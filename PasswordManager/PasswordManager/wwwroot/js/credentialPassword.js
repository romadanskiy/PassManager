window.onload = function () {
    let tds = document.getElementsByClassName('credential-password-click');
    for (let i = 0; i < tds.length; i++) {
        let input = tds[i].firstElementChild;
        tds[i].addEventListener('click', () => {
            if (input.getAttribute('type') === 'text')
                input.setAttribute('type', 'password');
            else
                input.setAttribute('type', 'text');
        })
    }
}