async function performRegister() {
    const username = document.getElementById('register-username').value;
    const password = document.getElementById('register-password').value;
    const email = document.getElementById('register-email').value;

    const registrationResult = await register(username, password, email);

    // Verificar el resultado del registro y realizar acciones adicionales si es necesario
    if (registrationResult) {
        console.log("Registro exitoso:", registrationResult);
        // Redirigir a la página de inicio de sesión después del registro exitoso
        window.location.href = "./indexLogin.html";
    } else {
        console.log("Error al registrar usuario");
        // Mostrar un mensaje de error o realizar acciones adicionales si es necesario
    }
}

async function register(username, password, email) {
    const url = "http://localhost:5271/User/register";
    let newUser = {
        username: username,
        password: password,
        email: email
    };

    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
            // ,
            // 'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(newUser)
    };

    try {
        const response = await fetch(url, options);
        const result = await response.text();
        return result;
    } catch (error) {
        console.error(error);
        return null;
    }
}

// Ejemplo de cómo llamar a la función de registro


// Asignar esta función al evento de clic del botón de registro en tu interfaz de usuario
// Ejemplo: document.getElementById('registerButton').addEventListener('click', performRegister);


function handleLogin() {
    // Obtener los valores de los campos de entrada
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    // Llamar a la función login con los valores obtenidos
    performLogin(username, password);
}



async function login(username, password) {
    const url = "http://localhost:5271/User/login";
    let update = {
        username: username,
        password: password,

    };
    console.log(update)
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(update)

    };
    console.log(options)
    try {
        const response = await fetch(url, options);
        const result = await response.text();
        return (result);
    } catch (error) {
        console.error(error);
        return null;
    }

}
async function performLogin(usernameLogin, passwordLogin) {
    const token = await login(usernameLogin, passwordLogin);
    console.log(token);

    // Verificar si el token es válido y contiene el campo success
    let tokenparse = JSON.parse(token);
    if (token.success) {
        console.log("Login exitoso. Token:", tokenparse.token);
        // Realizar acciones adicionales si es necesario
         // Redirigir a otro archivo
    }
    setTimeout(console.log("prueba"), 1000);
    localStorage.setItem('token', tokenparse.token);
    const base64Url = token.split('.')[1]; // Obtiene la parte base64 del token
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/'); // Reemplaza caracteres especiales
    const jsonPayload = decodeURIComponent(atob(base64).split('').map((c) => {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
    const decodedToken = JSON.parse(jsonPayload);
    console.log(decodedToken.roles);
    window.location.href = './indexMain.html';
}



