function getElementWidth(elementSelector) {
    var selectors = document.querySelectorAll(elementSelector);
    return selectors[0].clientWidth;
}

function getElementHeight(elementSelector) {
    var selectors = document.querySelectorAll(elementSelector);
    return selectors[0].clientHeight;
}
