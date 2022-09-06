const togglePassword = document.querySelector('#togglePassword');
  const password = document.querySelector('#id_password');
  const icon = document.querySelector('.svg-icon');

  togglePassword.addEventListener('click', function (e) {
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    icon.classList.toggle('svg-icon-primary')
});
