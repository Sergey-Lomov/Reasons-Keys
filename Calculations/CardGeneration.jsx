//--------------------- XML Constants
// Card
const deckElement = "Deck";
const cardElement = "Card";

const idElement = "id";
const mbElement = "mining_bonus";
const siElement = "stability_increment";
const paElement = "provides_artifact";
const isKeyElement = "is_key";
const mscElement = "min_stability_constraint";
const weightElement = "weight";
const usabilityElement = "uisability";

// BracnhPoint
const branchPointsElement = "BranchPoints";
const bpSuccessElement = "Success";
const bpFailedElement = "Failed";
const bpElement = "BranchPoint";

const branchElement = "branch";
const pointsElement = "points";
        
// Relation
const relationsElement = "Relations";
const relationElement = "Relation";
const positionElement = "position";

const typeElement = "type";
const reasonType = "reason";
const pairedReasonType = "paired_reason";
const blockerType = "blocker";

const directionElement = "direction";
const frontDirection = "front";
const backDirection = "back";

//--------------------- Layers Constants
const markupLayerName = "Markup";
const orientatedName = "Orientated";
const circularName = "Circular";
const orientatedTextFrameName = "Value"
const circularValuesGroupName = "Values";

const keyIndicatorLayer = "KeyIndicator";
const artifactIndicatorLayer = "ArtifactIndicator";

const miningBonusLayer = "MiningBonus";
const stabilityIncrementLayer = "StabilityIncrement";

const minStabilityConstraintLayer = "MinStabilityConstraint";
const minStabilityConstraintValue = "Value";

const circularKeyEventBPLayerName = "CircularKeyEventBP";
const failedBPLayerName = "FailedBP";
const successBPLayerName = "SuccessBP";
const hasValueGroupName = "HasValue";
const noValueLayerName = "Empty";
const valueLayerName = "Value";
const playersColorsLayerName = "PlayersColors";
const playersColorsPrefix = "p";
const signGroupName = "Sign";
const positivePointsPathName = "+";
const negativePointsPathName = "-";

const relationsLayerName = "Relations";
const reasonGroupName = "Reason";
const blockerGroupName = "Blocker";
const backGroupName = "In";
const frontGroupName = "Out";
const pairedPathName = "CoReason";

const weightLayerName = "Weight";
const usabilityLayerName = "Usability";
const idLayerName = "Id";
const weightValue = "Value";
const usabilityValue = "Value";
const idValue = "Value";

var outputInit = "/d/%D0%A0%D0%B0%D0%B7%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D0%BA%D0%B0/%D0%91%D1%83%D0%BC%D0%B0%D0%B3%D0%B0/%D0%9A%D0%BB%D1%8E%D1%87%D0%B8%20%D0%BF%D1%80%D0%B8%D1%87%D0%B8%D0%BD/%D0%A3%D0%BF%D1%80%D0%BE%D1%89%D0%B5%D0%BD%D0%BD%D0%B0%D1%8F/%D0%93%D1%80%D0%B0%D1%84%D0%B8%D0%BA%D0%B0/%D0%9A%D0%B0%D1%80%D1%82%D1%8B";
var filesPrefix = "card";
var useCircular = true;
var showSystemInfo = false;
var previewMode = true;
var saveAI = true;

function HandleSystemInfo (show, doc)
{
    doc.layers.getByName(idLayerName).visible = show;
    doc.layers.getByName(usabilityLayerName).visible = show;
    doc.layers.getByName(weightLayerName).visible = show;
}

 // Test
function HandleTextValue (mainLayerName, textFrameName, value, doc)
{
    var layer = doc.layers.getByName(mainLayerName);
    layer.visible = value.toString() != "0";
    layer.textFrames.getByName(textFrameName).contents = value.toString();
}

 // Markup
function HandleMarkup  (useCircular, doc)
{
    var layer = doc.layers.getByName(markupLayerName);
    var circularGroup = layer.groupItems.getByName(circularName);
    var orientatedGroup = layer.groupItems.getByName(orientatedName);
    circularGroup.hidden = !useCircular;
    orientatedGroup.hidden = useCircular;
}

 // Bool
 function HandleBoolValue(layerName, value, doc)
 {
     doc.layers.getByName(layerName).visible = value == "True";
 }

 // Numbers
function HandleNumberValue  (mainLayerName, useCircular, value, doc)
{
    var mainLayer = doc.layers.getByName(mainLayerName);
    if (!mainLayer.visible)
        return;
    
    var circularGroup = mainLayer.groupItems.getByName(circularName);
    var orientatedGroup = mainLayer.groupItems.getByName(orientatedName);
    circularGroup.hidden = !useCircular;
    orientatedGroup.hidden = useCircular;
    
    if (useCircular) 
        HandleCircularNumberValue(circularGroup, value); 
    else 
        HandleOrientatedNumberValue (orientatedGroup, value);
}

function HandleCircularNumberValue (circularGroup, value)
{
    circularGroup.hidden = value.toString() == "0";
    var valuesGroup = circularGroup.groupItems.getByName(circularValuesGroupName);      
    for (var i = 1; i <= valuesGroup.groupItems.length; i++)     
    {
        var valueItem = valuesGroup.groupItems.getByName(i);
        valueItem.hidden = i != parseInt (value.toString());
    }
}

function HandleOrientatedNumberValue (orientatedGroup, value)
{
    orientatedGroup.hidden = value.toString() == "0";
    orientatedGroup.textFrames.getByName(orientatedTextFrameName).contents = value.toString();
}
 
 // Branchpoints
function HandleBrachPoints (mainLayerName, useCircular, value, doc)
{
    var mainLayer = doc.layers.getByName(mainLayerName);
    var circularLayer = mainLayer.layers.getByName(circularName);
    var orientatedLayer = mainLayer.layers.getByName(orientatedName);
    circularLayer.visible = useCircular;
    orientatedLayer.visible = !useCircular;
    var modeLayer = useCircular ? circularLayer : orientatedLayer;
    
    var noValueLayer = modeLayer.layers.getByName(noValueLayerName);
    var hasValueGroup = modeLayer.groupItems.getByName(hasValueGroupName);
    var items = value.child(bpElement);
    noValueLayer.visible = items.length() == 0;
    hasValueGroup.hidden = items.length() == 0;    
    
    if (items.length() != 0) 
    {
        var branch = items[0].child(branchElement);
        var points = items[0].child(pointsElement);    
        
        var playersColorsGroup = hasValueGroup.groupItems.getByName(playersColorsLayerName);
        for (var i = 0; i < playersColorsGroup.pathItems.length; i++)
        {
            var playerColorItem = playersColorsGroup.pathItems.getByName(playersColorsPrefix + i);
            playerColorItem.hidden = i != parseInt (branch.toString());
        }
        
        if (useCircular) 
        {
            HandleCircularPoints(hasValueGroup, points);
        } 
        else 
        {
            HandleOrientatedPoints (hasValueGroup, points);
        }
    }
}
 
function HandleCircularPoints (mainGroup, points) 
{
    var signGroup = mainGroup.groupItems.getByName(signGroupName);
    var positivePath = signGroup.pathItems.getByName(positivePointsPathName);
    var negativePath = signGroup.pathItems.getByName(negativePointsPathName);
    positivePath.hidden = points < 0;
    negativePath.hidden = points > 0;
}
 
function HandleOrientatedPoints (mainGroup, points)
{
            // TODO: add "+" prefix for positive values
            mainGroup.textFrames.getByName(valueLayerName).contents = points.toString();
}

function HandleCircularKeySuccessPoints (success, useCircular, isKey, doc)
{
    var items = success.child(bpElement);
    if (items.length() > 0 && useCircular && isKey == "True") 
    {
        var points = items[0].child(pointsElement)
        doc.layers.getByName(circularKeyEventBPLayerName).visible = true;
        HandleNumberValue(circularKeyEventBPLayerName, true, points, doc);
    }
    else
    {
        doc.layers.getByName(circularKeyEventBPLayerName).visible = false;
    }
}

// Relations
function HandleRelations (relations, doc)
{
    // Comented code related to old paired reasons design
      var items = relations.child(relationElement);
      var mainGroup = doc.layers.getByName(relationsLayerName).groupItems;
      for (var i = 0; i < mainGroup.length; i++)
      {
            mainGroup.getByName(i).hidden = true;
      }
  
//      var minPaired = -1;
      for each(var relation in items)
      {
          var position = relation.child(positionElement).toString();
          var groupItem = mainGroup.getByName(position);
          groupItem.hidden = false;
          var reasonGroup = groupItem.groupItems.getByName(reasonGroupName);
          var blockerGroup = groupItem.groupItems.getByName(blockerGroupName);

          var type = relation.child(typeElement).toString() 
          var isReason = type != blockerType
          reasonGroup.hidden = !isReason;
          blockerGroup.hidden = isReason;
          var activeGroup = isReason ? reasonGroup : blockerGroup;
          
          var isBack = relation.child(directionElement).toString() == backDirection;
          var backGroup = activeGroup.pathItems.getByName(backGroupName);
          var frontGroup = activeGroup.pathItems.getByName(frontGroupName);    
          backGroup.hidden = !isBack;
          frontGroup.hidden = isBack;
          
          var pairedReasonPath = reasonGroup.pathItems.getByName(pairedPathName)
          alert(type)
          pairedReasonPath.hidden = type != pairedReasonType;
 /*         if (relation.child(typeElement).toString() == pairedReasonType)
          {
              minPaired = minPaired < parseInt (position) && minPaired  != -1 ? minPaired : parseInt (position);
          }*/
      }
 /*    if (minPaired != -1) 
     {
         var reasonGroup = mainGroup.getByName(minPaired).groupItems.getByName(reasonGroupName);
         reasonGroup.pathItems.getByName(pairedPathName).hidden = false;
     }*/
}

 //Saving
 
function ExportFileToAI(file, doc) {
    var originalInteractionLevel = userInteractionLevel;
    userInteractionLevel = UserInteractionLevel.DONTDISPLAYALERTS;    

    var ai8Doc = new File(file);
    var saveOptions = new IllustratorSaveOptions();
    saveOptions.embedICCProfile = true;

    app.activeDocument.saveAs(ai8Doc, saveOptions);
    
    userInteractionLevel = originalInteractionLevel;
}

function ExportFileToPDF(file, doc) 
{
    var originalInteractionLevel = userInteractionLevel;
    userInteractionLevel = UserInteractionLevel.DONTDISPLAYALERTS;

    var saveName = new File ( file );
    saveOpts = new PDFSaveOptions();
    saveOpts.compatibility = PDFCompatibility.ACROBAT5; 
    saveOpts.generateThumbnails = true; 
    saveOpts.preserveEditability = true;
    doc.saveAs( saveName, saveOpts );    
    
    userInteractionLevel = originalInteractionLevel;
}

function ExportFileToPNG24(file, doc) {
    var exportOptions = new ExportOptionsPNG24();
    exportOptions.antiAliasing = true;
    exportOptions.transparency = false;
    exportOptions.artBoardClipping = false;
    exportOptions.horizontalScale = 300;
    exportOptions.verticalScale = 300;
    
    var type = ExportType.PNG24;
    var fileSpec = new File(file);

    doc.exportFile(fileSpec, type, exportOptions);
}

function ExportFileToJPEG(file, doc) {
   /*var exportOptions = new ExportOptionsJPEG();
    exportOptions.qualitySetting = 100;

    var type = ExportType.JPEG;
    var fileSpec = new File(file);

    doc.outputResolution = 300;
    doc.exportFile(fileSpec, type, exportOptions);*/
var fileSpec = new File(file);

var captureOptions = new ImageCaptureOptions();
captureOptions.resolution = 300;
captureOptions.antiAliasing = true;

var curBoard = doc.artboards[doc.artboards.getActiveArtboardIndex()];
var captureClip = curBoard.artboardRect;
alert(doc.documentColorSpace);
doc.imageCapture(fileSpec, captureClip,captureOptions);
}

// TEST CODE SECTION
 var antilag = 0;
function exportJPG(pth){ //Export 300dpi CMYK jpg

  var str = "";

  for (var i=0;i<pth.length;i++) str += u16to8(pth.charCodeAt(i));

  var act = "/version 3/name [ 4 73657431]/isOpen 1"

  + "/actionCount 1/action-1 {/name [ 4 61637431]/keyIndex 0/colorIndex 0/isOpen 1/eventCount 1"

  + "/event-1 {/useRulersIn1stQuadrant 0/internalName (adobe_exportDocument)"

  + "/isOpen 0/isOn 1/hasDialog 1/showDialog 0/parameterCount 7"

  + "/parameter-1 {/key 1885434477/showInPalette 0/type (raw)"

  + "/value < 100 0a00000001000000030000000100000000002c01020000000000000001000000"

  + "69006d006100670065006d006100700000000000000000000000000000000000"

  + "0000000000000000000000000000000000000000000000000000000000000000"

  + "00000100>/size 100}" //Probably, parameter for exporter plugin

  + "/parameter-2 {/key 1851878757/showInPalette 4294967295"

  + "/type (ustring)/value [ " + str.length/2 + " " + str + "]}"

  + "/parameter-3 {/key 1718775156/showInPalette 4294967295"

  + "/type (ustring)/value [ 16 4a5045472066696c6520666f726d6174]}" // JPEG file format

  + "/parameter-4 {/key 1702392942/showInPalette 4294967295"

  + "/type (ustring)/value [ 12 6a70672c6a70652c6a706567]}" //jpg,jpe,jpeg

  + "/parameter-5 {/key 1936548194/showInPalette 4294967295/type (boolean)/value 0}"

  + "/parameter-6 {/key 1935764588/showInPalette 4294967295/type (boolean)/value 1}"

  + "/parameter-7 {/key 1936875886/showInPalette 4294967295/type (ustring)/value [ 0]}}}";

  var tmp = File(Folder.desktop + "/set1.aia");  

  tmp.open('w');  

  tmp.write(act); 

  tmp.close();

  app.loadAction(tmp); 

  app.doScript("act1", "set1", false);  

  app.unloadAction("set1","");

  tmp.remove();

if (antilag == 12)
{
    antilag = 0;
    alert("Antilag");
    }
else {
    antilag++;
    }

  }

function u16to8(cd) {

  var out =

  (cd < 0x80

  ? toHex2(cd)

  : (cd < 0x800

  ? toHex2(cd >> 6 & 0x1f | 0xc0)

  : toHex2(cd >> 12 | 0xe0) +

  toHex2(cd >> 6 & 0x3f | 0x80)

  ) + toHex2(cd & 0x3f | 0x80)

  );

  return out;

  }

function toHex2(num) {

  var out = '0' + num.toString(16);

  return out.slice(-2);

}

// TEST CODE SECTION END


function HandleCard (card, doc, folder)
{
      //Get values
      var id = card.child(idElement);
      var mb = card.child(mbElement);
      var si = card.child(siElement);
      var pa = card.child(paElement);
      var isKey = card.child(isKeyElement);
      var msc = card.child(mscElement);
      var weight = card.child(weightElement);
      var usability = card.child(usabilityElement);
      
      var branchPoints = card.child(branchPointsElement);
      var success = branchPoints.child(bpSuccessElement);
      var failed = branchPoints.child(bpFailedElement);
      
      var relations = card.child(relationsElement);
      
      //Update UI
      HandleSystemInfo(showSystemInfo, doc);
      HandleMarkup(useCircular, doc);
      
      //HandleBoolValue(keyIndicatorLayer, isKey, doc);
      HandleBoolValue(artifactIndicatorLayer, pa, doc);

      //HandleTextValue(minStabilityConstraintLayer, minStabilityConstraintValue, msc, doc);
      HandleTextValue(weightLayerName, weightValue, weight, doc);
      HandleTextValue(usabilityLayerName, usabilityValue, usability, doc);
      HandleTextValue(idLayerName, idValue, id, doc);

      HandleNumberValue(stabilityIncrementLayer, useCircular, si, doc);
      HandleNumberValue(miningBonusLayer, useCircular, mb, doc);
      HandleNumberValue(minStabilityConstraintLayer, useCircular, msc, doc);

      HandleBrachPoints(failedBPLayerName, useCircular, failed, doc);
      HandleBrachPoints(successBPLayerName, useCircular, success, doc);
      HandleCircularKeySuccessPoints(success, useCircular, isKey, doc);
      
      HandleRelations(relations, doc);
      
      //Save
     var filePath = folder.fsName + "\\" + filesPrefix + id
      if (previewMode) 
      {
          exportJPG(filePath + ".jpeg");
          // ExportFileToJPEG(filePath, doc);
          // ExportFileToPNG24(filePath, doc);
      } else if (saveAI) {
            ExportFileToAI(filePath, doc);
      } else {
            ExportFileToPDF(filePath, doc);
      }
}

function GenerateDeck ()
{
    if ( app.documents.length > 0 )
    {
        var doc = app.activeDocument;
        var file = File.openDialog ("Select cards XML", "*.xml;");
        var outputFolder = new Folder(outputInit).selectDlg("Select destination folder");

        file.open("r");
        var xmlContent = XML ( file.read() );
        for each(var card in xmlContent.elements()) {  
            HandleCard(card, doc, outputFolder);
        }
    }
    else
    {
        alert( "Please open a document with template." );
    }
}

GenerateDeck ();