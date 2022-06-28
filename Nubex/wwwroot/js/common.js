//##############################################################################################
//########### Toastr
window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", 'Turtle Bay Resort', { timeout: 5000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", 'Turtle Bay Resort', { timeout: 5000 });
    }
    if (type === "info") {
        toastr.info(message, "Operation Message", 'Turtle Bay Resort', { timeout: 5000 });
    }
    if (type === "warning") {
        toastr.warning(message, "Operation warning", 'Turtle Bay Resort', { timeout: 5000 });
    }
}

//##############################################################################################
//########### Sweet Alert
window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Success notification!',
            message,
            'success'
        )
    }
    if (type === "error") {
        let timerInterval
        Swal.fire({
            title: 'Auto close alert!',
            html: 'I will close in <b></b> milliseconds.',
            timer: 10000,
            timerProgressBar: true,
            didOpen: () => {
                Swal.showLoading()
                const b = Swal.getHtmlContainer().querySelector('b')
                timerInterval = setInterval(() => {
                    b.textContent = Swal.getTimerLeft()
                }, 100)
            },
            willClose: () => {
                clearInterval(timerInterval)
            }
        }).then((result) => {
            /* Read more about handling dismissals below */
            if (result.dismiss === Swal.DismissReason.timer) {
                console.log('I was closed by the timer')
            }
        })
    }
    if (type === "delete") {
        
        Swal.fire({

            title: message,
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            getConfirmButton: true
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success',
                )

                return true;
            }
            else {
                return false;
            }
        }).then(Swal.getConfirmButton())
    }
    if (type === "warning") {
        Swal.fire({
            title: 'Do you want to save the changes?',
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: 'Save',
            denyButtonText: `Don't save`,
            icon: 'warning',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                Swal.fire('Saved!', '', 'success')
            } else if (result.isDenied) {
                Swal.fire('Changes are not saved', '', 'info')
            }
        })
    }

    if (type === "getuser") {
        Swal.fire({
            title: message,
            input: 'text',
            inputAttributes: {
                autocapitalize: 'off'
            },
            showCancelButton: true,
            confirmButtonText: 'Look up',
            showLoaderOnConfirm: true,
            preConfirm: (login) => {
                return fetch(`//api.github.com/users/${login}`)
                    .then(response => {
                        if (!response.ok) {
                            throw new Error(response.statusText)
                        }
                        return response.json()
                    })
                    .catch(error => {
                        Swal.showValidationMessage(
                            `Request failed: ${error}`
                        )
                    })
            },
            allowOutsideClick: () => !Swal.isLoading()
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire({
                    title: `${result.value.login}'s avatar`,
                    imageUrl: result.value.avatar_url
                })
            }
        })
    }
}

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}