const token = localStorage.getItem('token');

async function login() {
    const url = "http://localhost:5271/Boss";

    const options = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
    };

    try {
        const response = await fetch(url, options);
        const result = await response.json();
        return result;
    } catch (error) {
        console.error(error);
        return null;
    }
}

async function performLogin() {
    const result = await login();
    console.log(result);
}

// Call performLogin when the HTML content and script are loaded
document.addEventListener('DOMContentLoaded', () => {
    performLogin();
});