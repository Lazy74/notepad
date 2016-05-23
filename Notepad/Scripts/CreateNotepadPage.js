var model = {

    createNotepad: function(name) {
        if (!name) {
            return;
        }
        console.log("function: createNotepad");
        $.post("/api/PageNotepad/Create", {
                name: name
            })
            .done(function () {
                alert("Блокнот добавлен");
            })
            .fail(function() {
                alert("Не удалось добавить блокнот!");
            });
    }
}

var viewModel1 = new ViewModel1();

function loadBody() {
    ko.applyBindings(viewModel1);  //возможность работать с моделью представления
}

function ViewModel1() {
    this.newNotepadName = ko.observable();

    this.createNotepad = function () {
        console.log("click: createNotepad");
        model.createNotepad(this.newNotepadName());
    };

    this.toStartPage = function () {
        location.pathname = "";
    }
}

