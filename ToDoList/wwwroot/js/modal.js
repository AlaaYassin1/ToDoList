
    function ShowModelAddEdit(modelid,dataObj,isedit=false)
    {
        let modelElm = document.getElementById(modelid);
    var myModal = new bootstrap.Modal(modelElm, {
        keyboard: false
            })
    console.log(dataObj)
    if(dataObj!==null)
    {
        modelElm.querySelector('#task_Id').value = dataObj.Id;

    modelElm.querySelector('#task_title').value = dataObj.title;

    modelElm.querySelector('#task_description').value = dataObj.description;
    modelElm.querySelector('#task_IsCompleted').value = dataObj.IsCompleted;

    try{
                        if( modelElm.querySelector('#task_Date')!==null)
    if (dataObj.Date===null)
    modelElm.querySelector('#task_Date').valueAsDate = null;
    else
    {
        let dateofbirth = new Date(dataObj.Date);
    dateofbirth.setDate(dateofbirth.getDate() + 1);
    modelElm.querySelector('#task_Date').valueAsDate = dateofbirth;
                        }
                    }catch(e){
        alert(e)
    }
                }

    toggleModelAction(modelElm,isedit);

    myModal.toggle()
        }

    function toggleModelAction(modelElm,isedit) {
                if (isedit === true) {
        modelElm.querySelector('#ptnadd').hidden = true;
    modelElm.querySelector('#ptnedit').hidden = false;
    modelElm.querySelector('div.modal-header h5.modal-title').innerHTML = "Edit Task";

    modelElm.querySelector('#task_Id').readOnly = true;
    modelElm.querySelector('#task_Id').style.color = "#c0c0c0";

                }
    else {
        modelElm.querySelector('#ptnadd').hidden = false;
    modelElm.querySelector('#ptnedit').hidden = true;
    modelElm.querySelector('div.modal-header h5.modal-title').innerHTML = "Add Task";
    modelElm.querySelector('#task_Id').readOnly = false;
    modelElm.querySelector('#task_Id').style.color = "#000";
                }
            }

    function DeleteConfirm(tagElm){
        let delForm=tagElm.parentElement|| null;
    Swal.fire({
        title: 'Are you sure?',
    text: "You won't be able to revert this!",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
     if(delForm!==null){
        delForm.submit();
     }
    Swal.fire(
    'Deleted!',
    'Your file has been deleted.',
    'success'
    )
  }
})
    }
