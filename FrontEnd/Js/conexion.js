const DataMethod = {
    method: 'GET'
};
const DataConexion = async (DataMethod) => {
    try {
        const peticion = await fetch(`http://localhost:5271/Boss`, DataMethod);
        const results = await peticion.json();
        let consults = '';
        results.consults.forEach(respon => {
            consults += `
                <div class="pelicula">
                    <h3 class="titulo">${respon.Name}</h3>
                </div>
            `;
        });

        document.getElementById('contenedor').innerHTML = peliculas;
    } catch (error) {
        console.error('Error:', error);
    }
};

(async () => {
    const data = await DataConexion(DataMethod);
    console.log(data);
})();

