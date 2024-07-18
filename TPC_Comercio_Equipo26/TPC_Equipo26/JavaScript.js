
function mostrarConfirmacion() {
    Swal.fire({
        title: 'Mi Perfil',
        text: 'Los cambios se han guardado correctamente.',
        icon: 'success',
        confirmButtonText: 'OK'
    });
    return true;
}

//Validación front para LogIn: Si está todo vacío muestra divError, si hay un campo vacio, lblError
//Si estan mal las credenciales sigue mandando a error.aspx con "user o pass incorrectos" > antes hace una pausa de 1.5 segundos
//Si estan ok las credenciales se ve el doble check verde y redirecciona > antes hace una pausa de 1.5 segundos
function ValidarLogIn() {
    const txtUser = document.getElementById("txtUser");
    const txtPassword = document.getElementById("txtPassword");
    const errorDiv = document.getElementById("errorDiv");

    let isValid = true;
    let errorMessage = "";

    if (txtUser.value.trim() === "" && txtPassword.value.trim() === "") {
        errorMessage = "Por favor, complete todos los campos.";
        isValid = false;
    } else {
        if (txtUser.value.trim() === "") {
            txtUser.classList.add("is-invalid");
            txtUser.classList.remove("is-valid");
            errorMessage = "Por favor, ingrese su usuario.";
            isValid = false;
        } else {
            txtUser.classList.add("is-valid");
            txtUser.classList.remove("is-invalid");
        }

        if (txtPassword.value.trim() === "") {
            txtPassword.classList.add("is-invalid");
            txtPassword.classList.remove("is-valid");
            errorMessage = "Por favor, ingrese su contraseña.";
            isValid = false;
        } else {
            txtPassword.classList.add("is-valid");
            txtPassword.classList.remove("is-invalid");
        }
    }

    if (!isValid) {
        errorDiv.innerHTML = errorMessage;
        errorDiv.style.display = "block";
    } else {
        errorDiv.style.display = "none";
    }

    return isValid;
}

function mostrarMensajeExito() {
    alert(mensaje);
    //setTimeout(function () {
    //    window.location.href = 'Default.aspx';
    //}/*, 1000*/);
}

function mostrarMensajeStock() {
    alert(mensaje);
    setTimeOut(function () {
        window.location.href = 'Ventas.aspx';
    });
}

function BuscarVacio(inputId) {
    var searchBox = document.getElementById(inputId);

    if (searchBox) {
        searchBox.addEventListener('input', function () {
            if (!searchBox.value) {
                __doPostBack(searchBox.id, '');
            }
        });
    }
}
//Reemplaza la funcion de arriba, muestra el cuadro como cuando guardas datos nuevos en MiPerfil.

//function mostrarMensajeExitoArticulo() {
//    Swal.fire({
//        title: 'Artículo nuevo',
//        text: '¡Artículo agregado con éxito!',
//        icon: 'success',
//        confirmButtonText: 'OK'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            window.location.href = 'Default.aspx';
//        };
//    return true;
//}

//function ValidarRegistro() {
//    const txtUser = document.getElementById("txtUser");
//    const txtEmail = document.getElementById("txtEmail");
//    const txtPassword = document.getElementById("txtPassword");

//    const lblUser = document.getElementById("lblUser");
//    const lblEmail = document.getElementById("lblEmail");
//    const lblPassword = document.getElementById("lblPassword");

//    let isValid = true;
//    let errorMessage = "";

//    if (txtUser.value.trim() === "" && txtEmail.value.trim() === "" && txtPassword.value.trim() === "") {
//        errorMessage = "Por favor, complete todos los campos.";
//        isValid = false;
//    } else if (txtUser.value.trim() === "" && txtEmail.value.trim() !== "" && txtPassword.value.trim() !== "") {
//        lblUser.innerText = "Por favor, ingrese un nombre de usuario.";
//        isValid = false;
//    } else if (txtEmail.value.trim() === "" && txtUser.value.trim() !== "" && txtPassword.value.trim() !== "") {
//        lblEmail.innerText = "Por favor, ingrese un correo de electrónico.";
//        isValid = false;
//    } else {
//        lblPassword.innerText = "Por favor, ingrese una contraseña.";
//        isValid = false;
//    }

//    if (txtUser.value.length < 4) {
//        lblUser.innerText = "El nombre de usuario debe contener al menos 4 caracteres.";
//        txtUser.classList.add("is-invalid");
//        txtUser.classList.remove("is-valid");
//        isValid = false;
//    }

//    if (txtPassword.value.length < 4 || !/\d/.test(txtPassword.value)) {
//        lblPassword.innerText = "La contraseña debe contener al menos 4 caracteres y al menos 1 caracter numérico.";
//        txtPassword.classList.add("is-invalid");
//        txtPassword.classList.remove("is-valid");
//        isValid = false;
//    }

//    if (!isValid) {
//        errorDiv.innerHTML = errorMessage;
//        errorDiv.style.display = "block";
//    } else {
//        errorDiv.style.display = "none";
//    }

//    return isValid;
//}


//Validación front para Registro > Se pierde porque pasa antes por las validaciones de las clases
//function ValidarRegistro() {
//    const txtUser = document.getElementById("txtUser");
//    const txtEmail = document.getElementById("txtEmail");
//    const txtPassword = document.getElementById("txtPassword");

//    const lblUser = document.getElementById("lblUser");
//    const lblEmail = document.getElementById("lblEmail");
//    const lblPassword = document.getElementById("lblPassword");

//    let isValid = true;
//    let errorMessage = "";

//    if (txtUser.value.trim() === "" && txtEmail.value.trim() === "" && txtPassword.value.trim() === "") {
//        errorMessage = "Por favor, complete todos los campos.";
//        isValid = false;

//        if (txtUser.value.trim() === "") {
//            txtUser.classList.add("is-invalid");
//            txtUser.classList.remove("is-valid");
//            errorMessage = "Por favor, ingrese un nombre de usuario.";
//            isValid = false;
//        } else if (txtUser.value.length < 4) {
//            lblUser.innerText = "El nombre de usuario debe contener al menos 4 caracteres.";
//            txtUser.classList.add("is-invalid");
//            txtUser.classList.remove("is-valid");
//            isValid = false;
//        } else {
//            lblUser.innerText = "";
//            txtUser.classList.add("is-valid");
//            txtUser.classList.remove("is-invalid");
//        }

//        if (txtEmail.value.trim() === "") {
//            txtEmail.classList.add("is-invalid");
//            txtEmail.classList.remove("is-valid");
//            errorMessage = "Por favor, ingrese un correo de electrónico.";
//            isValid = false;
//        } else {
//            lblEmail.innerText = "";
//            txtEmail.classList.add("is-valid");
//            txtEmail.classList.remove("is-invalid");
//        }

//        if (txtPassword.value.trim() === "") {
//            txtPassword.classList.add("is-invalid");
//            txtPassword.classList.remove("is-valid");
//            errorMessage = "Por favor, ingrese una contraseña.";
//            isValid = false;
//        } else if (txtPassword.value.length < 4 || !/\d/.test(txtPassword.value)) {
//            lblPassword.innerText = "La contraseña debe contener al menos 4 caracteres y al menos 1 caracter numérico.";
//            txtPassword.classList.add("is-invalid");
//            txtPassword.classList.remove("is-valid");
//            isValid = false;
//        } else {
//            lblPassword.innerText = "";
//            txtPassword.classList.add("is-valid");
//            txtPassword.classList.remove("is-invalid");
//        }
//    }

//    if (!isValid) {
//        errorDiv.innerHTML = errorMessage;
//        errorDiv.style.display = "block";
//    } else {
//        errorDiv.style.display = "none";
//    }

//    return isValid;
//}


//function showModalStock() {
//    var modal = document.getElementById("myModal");
//    var span = document.getElementsByClassName("close")[0];
//    modal.style.display = "block";
//    span.onclick = function () {
//        modal.style.display = "none";
//    }
//    window.onclick = function (event) {
//        if (event.target == modal) {
//            modal.style.display = "none";
//        }
//    }
//    return false;
//}

//function showModalStock() {
//    console.log("showModalStock called");
//    var modal = document.getElementById("myModal");
//    var span = document.getElementsByClassName("close")[0];
//    modal.style.display = "block";
//    span.onclick = function () {
//        modal.style.display = "none";
//    }
//    window.onclick = function (event) {
//        if (event.target == modal) {
//            modal.style.display = "none";
//        }
//    }
//}




