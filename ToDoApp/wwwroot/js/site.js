// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*document.addEventListener("DOMContentLoaded", () => {
    const forms = document.querySelectorAll("form");

    forms.forEach(form => {
        form.addEventListener("submit", (e) => {
            const button = form.querySelector("button[type='submit']");
            if (button) {
                button.innerHTML = "Processing...";
                button.disabled = true;
            }
        });
    });
});
*/
document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("ToDo_Form");

     const inputError = document.getElementById("input_error");

    form.addEventListener("submit", (e) => {
        e.preventDefault();  

      
        const inputTask = document.getElementById("ToDo_Input");
        const inputValue = inputTask.value.trim();

        
        if (inputValue.length < 1) {
            inputError.innerText = "Please, fill in the input!";
            inputError.classList.add("text-danger");
            return;
        } else if (inputValue.length > 100) {
            inputError.innerText = "The maximum value is 100.";
            inputError.classList.add("text-danger");
            return;
        } else {
            inputError.innerText = "";
            inputError.classList.remove("text-danger");
        }

        
        console.log("Sending data...");
        e.target.submit(); 
    });
});

