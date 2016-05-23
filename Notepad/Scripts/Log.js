var viewModel1 = new ViewModel1();

function loadBody() {
    ko.applyBindings(viewModel1);  //возможность работать с моделью представления
}

function ViewModel1() {
    this.toStartPage = function () {
        location.pathname = "";
    }
}