window.keyNav = {
    _handler: null,
    registerEscape: function (dotNetRef) {
        this.unregisterEscape();
        this._handler = function (e) {
            if (e.key === "Escape") {
                dotNetRef.invokeMethodAsync("OnEscapePressed");
            }
        };
        document.addEventListener("keydown", this._handler);
    },
    unregisterEscape: function () {
        if (this._handler) {
            document.removeEventListener("keydown", this._handler);
            this._handler = null;
        }
    }
};
