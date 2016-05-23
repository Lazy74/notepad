console.log("start");

var model = {
    createNotepad: function (name) {
        console.log("function: createNotepad");
        if (!name) {
            alert("Для создания блокнота, нужно ввести новое название");
            return;
        }
        $.post("/api/PageNotepad/Create", { name: name })
            .done(function () {
                updateNotepads();
                alert("Блокнот добавлен");
            })
            .fail(function () {
                alert("Не удалось добавить блокнот!");
            });
    },

    getNotepad: function () {
        console.log("GetNotepads");
        return $.get("/api/PageNotepad/GetNotepads");
    },

    updateNotepad: function (name, content) {
        $.post('/api/PageNotepad/update', {
            name: name,
            content: content
            })
            .done(function () {
                alert("Блокнот обновлен");
            })
            .fail(function () {
                alert("Не удалось обновить блокнот!");
            });
    },

    getNotepadContent: function(name) {
        return $.get('/api/PageNotepad/GetNotepadContent/' + name);
    },

    delNotepad: function(name) {
        return $.get('/api/PageNotepad/DeleteNotepad/' + name);
    }
}

var viewModel1 = new ViewModel1();

function loadBody() {
    console.log("loadBody");
    ko.applyBindings(viewModel1);  //возможность работать с моделью представления
    updateNotepads();
}

function selectNotepad(notepad) {
    $('.selected').removeClass('selected');
    $(notepad).addClass('selected');
}

function ViewModel1() {
    this.notepadImageName = ko.observable('/Image/');
    this.newNotepadName = ko.observable();
    this.notepads = ko.observableArray();
    this.notepadContent = ko.observable();

    var that = this;

    that.currentNotepad;

    this.createNotepad = function () {
        console.log("click: createNotepad");
        model.createNotepad(this.newNotepadName());
        this.newNotepadName('');
    };

    this.loadNotepad = function (notepad) {
        console.log("loadNotepad");
        that.currentNotepad = notepad.name;
        that.notepadImageName('/Image/' + notepad.name);

        model.getNotepadContent(notepad.name)
            .done(function (content) {
                console.log("Получены данные блокнота: " + notepad.name);
                that.notepadContent(content);
            })
            .fail(function (r) {
                alert("Не удалось получить данные блокнота!");
                console.log("Ошибка получения данных блокнота " + r);
            });
    };

    this.saveContent = function () {
        if (!that.currentNotepad) {
            alert("Произошла ошибка, данные не будут сохранены!");
            return;
        }

        model.updateNotepad(this.currentNotepad, this.notepadContent());
    };

    this.deletNotepad = function() {
        if (!that.currentNotepad) {
            alert("Выберите блокнот для удаления");
            return;
        }

        model.delNotepad(that.currentNotepad)
            .done(function() {
                alert("Блокнот удален");
                updateNotepads();
                that.notepadImageName('/Image/');
                that.notepadContent('');
            })
            .fail(function() {
                alert("Блокнот не удален!");
            });
    };

    this.pageCreateNotepad = function() {
        location.pathname = "/notepad/create";
    };

    this.toLog = function () {
        location.pathname = "/log";
    }
}

function updateNotepads() {
    console.log("function updateNotepads()");
    model.getNotepad()
        .done(function (data) {
            console.log("function updateNotepads().done");
            viewModel1.notepads(data);
        })
        .fail(function () {
            console.log("function updateNotepads().fail");
            alert("Во время обновления блокнота что-то пошло не так!");
        });
}

