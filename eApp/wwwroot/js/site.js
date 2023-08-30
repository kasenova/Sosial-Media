
function signOut (){
    fetch("/User/SignOut", {
    method:"POST" ,
    })
    .then(response => {
        if (response.ok) {
            window.location.href = "/User/Login";
        }else {
            console.error("Sign-out failed");
        }
    })
    .catch(error => {
        console.error("Sign-out failed", error);
    });
}

