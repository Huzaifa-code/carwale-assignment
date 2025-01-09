const togglePasswordIcons = document.querySelectorAll(".hide-pass i");

const userName = document.getElementById('name')
const email = document.getElementById('email')
const pass = document.getElementById('pass')
const confirmPass = document.getElementById('confirm-pass')

//? For Password Field - Hide or Unhide
togglePasswordIcons.forEach(icon => {
    icon.addEventListener('click', () => {
        const inputField = icon.id === "hide-pass" ? pass : confirmPass;

        if (inputField.type === "password") {
            inputField.type = "text";
            icon.classList.remove("fa-eye-slash");
            icon.classList.add("fa-eye");
        } else {
            inputField.type = "password";
            icon.classList.remove("fa-eye");
            icon.classList.add("fa-eye-slash");
        }
    });
});


const validateUserName = () => {
    const name = userName.value;
    if ( name.length < 3 || name.length > 25  ){
        const nameError = document.getElementById('name-error')
        nameError.style.display = 'block';
        userName.style.border = '1px solid red';
        error = true;
        // console.log("Username Error")
    }else {
        const nameError = document.getElementById('name-error');
        nameError.style.display = 'none';
        userName.style.border = '2px solid rgb(13, 190, 4)';
        error = false;
    }
}

const validatePassword = () => {
    const passValue = pass.value;
    if(passValue.length < 8 || noCapitalLetter(passValue) || noSpecialCharacter(passValue) || noNumber(passValue)){
        const passError = document.getElementById('pass-error')
        passError.style.display = 'block';
        pass.style.border = '1px solid red';
        error = true;
        // console.log("Pass Error")
    }else {
        const passError = document.getElementById('pass-error')
        passError.style.display = 'none';
        pass.style.border = '2px solid rgb(13, 190, 4)';
        error = false;
    }
}

const validateConfirmPassword = () => {
    const confValue = confirmPass.value; // confirm password value
    const passValue = pass.value;
    const cpassError = document.getElementById('cpass-error')
    if(confValue !== passValue ){
        cpassError.style.display = 'block';
        confirmPass.style.border = '1px solid red';
        error = true;
    }else {
        cpassError.style.display = 'none';
        confirmPass.style.border = '2px solid rgb(13, 190, 4)'
        error = false
    }
}



// Validation functions for password :
function noCapitalLetter(pass){
    let isCapital = false;
    for(let i=0; i<pass.length; i++){
        p = pass[i];
        if(p >= 'A' && p <= 'Z' ){
            isCapital = true;
        }
    }

    if(!isCapital){
        console.log("No capital letter exit")
        return true; // no capital letter exist
    }

    return false; // capital letter exist
}

function noSpecialCharacter(pass){
    let isSpecial = false;
    for(let i=0; i<pass.length; i++){
        p = pass[i];
        if( p=='!' || p=='@' || p=='#' || p=='$' || p=='%' || p=='^' || p=='&' || p=='*' ){
            isSpecial = true;
        }
    };
    if(!isSpecial){
        console.log("No special char exit")
        return true; // no special  exist
    }
    return false; // capital letter exist
}

function noNumber(pass){
    let isNumber = false;
    for(let i=0; i<pass.length; i++){
        p = pass[i];
        if( p>=0 || p<=9 ){
            isNumber = true;
        }
    };
    if(!isNumber){
        console.log("No number exit")
        return true; // no number exist
    }
    return false; // number exist
}


// Validation
const validation = (userName, email, pass, confirmPass,error) => {
    validateUserName();

    // Email Validation 

    // Password validation
    validatePassword()

    // Confirm Password Validation
    validateConfirmPassword()

}


const form = document.getElementById('signup-form');

form.addEventListener('submit', (e) => {
    e.preventDefault();
    const userName = document.getElementById('name')
    const email = document.getElementById('email')
    const pass = document.getElementById('pass')
    const confirmPass = document.getElementById('confirm-pass')
    let error = false;

    
    // Validation :
    validation(userName, email, pass, confirmPass,error)

    if(error){
        return ;
    }

    const successModal = document.getElementById('success-modal');
    successModal.style.display = 'flex'
    // alert('You have successfully signed up!');
})


// Adding onChange Event Listeners
userName.addEventListener('input', (e) => {
    validateUserName()
})

pass.addEventListener('input', (e) => {
    validatePassword();
})

confirmPass.addEventListener('input', (e) => {
    validateConfirmPassword();
})

