//--------------------- Layers Constants
const miningLayerName = "Content";
const makeupName = "Makeup";
const backLayerName = "Back"

var miningLayer = app.activeDocument.layers.getByName(miningLayerName)
var makeupLayer = app.activeDocument.layers.getByName(makeupName)
var backLayer = app.activeDocument.layers.getByName(backLayerName)

for (var i = miningLayer.layers.length - 1; i >= 0; i--) {
         miningLayer.layers[i].remove()
}

for (var i = makeupLayer.layers.length - 1; i >= 0; i--) {
         makeupLayer.layers[i].remove()
}

for (var i = backLayer.layers.length - 1; i >= 0; i--) {
         backLayer.layers[i].remove()
}
