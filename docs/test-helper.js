function getElementWidth(elementSelector) {
    var selectors = document.querySelectorAll(elementSelector);
    return selectors[0].clientWidth;
}

function getElementHeight(elementSelector) {
    var selectors = document.querySelectorAll(elementSelector);
    return selectors[0].clientHeight;
}

function dispatchKeydown(keyCode, ctrlKey, shiftKey, altKey, metaKey) {
    var keyboardEvent = new KeyboardEvent('keydown', {
        keyCode: keyCode,
        ctrlKey: ctrlKey,
        shiftKey: shiftKey,
        altKey: altKey,
        metaKey: metaKey,
        bubbles: true,
        cancelable: true
    });
    document.activeElement.dispatchEvent(keyboardEvent);
}

function focusElement(elementSelector) {
    var selectors = document.querySelectorAll(elementSelector);
    selectors[0].focus();
}

function blurElement(elementSelector) {
    var selectors = document.querySelectorAll(elementSelector);
    selectors[0].blur();
}
