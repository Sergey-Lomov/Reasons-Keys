//--------------------- XML Constants
// Card
const nodeElement = "Node";
const layerElement = "layer";
const xElement = "x";
const yElement = "y";

//--------------------- Layers Constants
const prototypeLayerName = "Prototype";
const miningLayerName = "Mining";
const makeupName = "Makeup";
const backLayerName = "Back"

function CopyLayer (layer, toLayer, doc) 
{
    var newLayer = toLayer.layers.add();
    newLayer.name = layer.name

    for (var i = 0; i < layer.layers.length; i++) {
         CopyLayer (layer.layers[i], newLayer, doc)
    }

    for (var i = layer.pageItems.length - 1; i >= 0; i--) {
        layer.pageItems[i].duplicate(newLayer)
    }

    newLayer.zOrder(ZOrderMethod.SENDBACKWARD)
    
    return newLayer
}

function MoveLayerContent(layer, translate)
{
    for (var i = 0; i < layer.layers.length; i++) {
         MoveLayerContent (layer.layers[i], translate)
    }

    for (var i = 0; i < layer.pageItems.length; i++) {
        layer.pageItems[i].translate(translate[0], translate[1])
    }
}

function move(layer, x, y) 
{
    var center = LayerCenter (layer)
    var translate = [x - center[0], y - center[1]]
    MoveLayerContent(layer, translate)
}

function LayerBounds(layer)
{
    var bounds = [10000, -10000, -10000, 10000]
    
    for (var i = 0; i < layer.pageItems.length; i++) {
        var lbounds = layer.pageItems[i].geometricBounds
        if (lbounds[0] < bounds[0]) bounds[0] = lbounds[0]
        if (lbounds[1] > bounds[1]) bounds[1] = lbounds[1]
        if (lbounds[2] > bounds[2]) bounds[2] = lbounds[2]
        if (lbounds[3] < bounds[3]) bounds[3] = lbounds[3]
    }

    for (var i = 0; i < layer.layers.length; i++) {
        var lbounds = LayerBounds (layer.layers[i])
        if (lbounds[0] < bounds[0]) bounds[0] = lbounds[0]
        if (lbounds[1] > bounds[1]) bounds[1] = lbounds[1]
        if (lbounds[2] > bounds[2]) bounds[2] = lbounds[2]
        if (lbounds[3] < bounds[3]) bounds[3] = lbounds[3]
    }

    return bounds
}

function LayerCenter(layer) 
{
    var bounds = LayerBounds(layer)
    var x = (bounds[0] + bounds[2]) / 2
    var y = (bounds[1] + bounds[3]) / 2
    return [x, y]
}

function HandleNode (node, doc)
{
    if( typeof HandleNode.counter == 'undefined' ) {
        HandleNode.counter = 0;
    }

    //Get values
    var layerName = node.child(layerElement)
    var x = node.child(xElement)
    var y = -1 * node.child(yElement)
      
    var protypeLayer = doc.layers.getByName(prototypeLayerName)
    var protypeMiningLayer = protypeLayer.layers.getByName(miningLayerName)
    var protypeMakeupLayer = protypeLayer.layers.getByName(makeupName)
    var protypeBackLayer = protypeLayer.layers.getByName(backLayerName)
      
    var miningLayer = doc.layers.getByName(miningLayerName)
    var makeupLayer = doc.layers.getByName(makeupName)
    var backLayer = doc.layers.getByName(backLayerName)

    var newBack = CopyLayer (protypeBackLayer, backLayer, doc)
    newBack.name = HandleNode.counter
    move(newBack, x, y)
    
    var newMakeup = CopyLayer (protypeMakeupLayer, makeupLayer, doc)
    newMakeup.name = HandleNode.counter
    move(newMakeup, x, y)

    var nodeMiningLayer = protypeMiningLayer.layers.getByName(layerName)
    var newMining = CopyLayer (nodeMiningLayer, miningLayer, doc)
    move(newMining, x, y)

    HandleNode.counter++
}

function GenerateField ()
{
    if ( app.documents.length > 0 )
    {
        var doc = app.activeDocument
        var file = File.openDialog ("Select field XML", "*.xml;")

        file.open("r")
        var xmlContent = XML ( file.read() )
        for each(var node in xmlContent.elements()) {  
            HandleNode(node, doc)
        }
    }
    else
    {
        alert( "Please open a document with template." )
    }
}

GenerateField ()