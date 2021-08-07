$(function () {
    //abp.localization.getResource: used to localize text using the same JSON file
    var bs = abp.localization.getResource('BookStore');

    //add new path
    //manage modals in the client side
    var createModal = new abp.ModalManager(abp.appPath + 'Books/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Books/EditModal');

    var dataTable = $('#BooksTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({ //Helper function; it simplifies the Datatables configuration by providing conventional default values for missing options.
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,                      //cade.bookStore.books is namespace with transforming BookAppService
            ajax: abp.libs.datatables.createAjax(cade.bookStore.books.book.getList), //adapt ABP's dynamic JavaScript API proxies to Datatable's expected parameter format
            columnDefs: [    //Added a new column at the beginning of the columnDefs section.
                {
                    title: bs('Actions'),
                    rowAction: {
                        items:
                            [ //per row
                                {   //edit function
                                    text: bs('Edit'),
                                    //"abp.auth.isGranted()" checks a permission that is defined
                                    visible: abp.auth.isGranted('BookStore.Books.Update'),   //Whether the control displays the editing function
                                    action: function (data) {
                                        editModal.open({id: data.record.id});   //open the edit dialog
                                    }
                                },
                                {   //delete function
                                    text: bs('Delete'),
                                    visible: abp.auth.isGranted('BookStore.Books.Delete'),  //Whether the control displays the deleting function
                                    confirmMessage: function (data) {
                                        return bs('BookDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        cade.bookStore.books.book
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(bs('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: bs('Name'),
                    data: "name"
                },
                {
                    title: bs('Type'),
                    data: "type",
                    render: function (data) {
                        return bs('Enum:BookType:' + data);
                    }
                },
                {
                    title: bs('PublishDate'),
                    data: "publishDate",
                    render: function (data) {
                        return luxon.DateTime.fromISO(data, {   //luxon deal dates and times.
                            locale: abp.localization.currentCulture.name
                        }).toLocaleString();
                    }
                },
                {
                    title: bs('Price'),
                    data: "price"
                },
                {
                    title: bs('CreationTime'),
                    data: "creationTime",
                    render: function (data) {
                        return luxon.DateTime.fromISO(data, {
                            locale: abp.localization.currentCulture.name
                        }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }
            ]
        })
    );

    ////add new path
    ////manage modals in the client side
    //var createmodal = new abp.modalmanager(abp.apppath + 'books/createmodal');

    //used to refresh the data table after creating a new book.
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewBookButton').click(function (e) {
        console.log("Invoke Index button");
        e.preventDefault();
        createModal.open();   //used to open the model to create a new book.
    })


    //callback refreshes the data table when you close the edit modal.
    editModal.onResult(function () {
        dataTable.ajax.reload();
    });
});