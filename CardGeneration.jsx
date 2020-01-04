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
const pairedGroupName = "CoReason";

const weightLayerName = "Weight";
const usabilityLayerName = "Usability";
const idLayerName = "Id";
const weightValue = "Value";
const usabilityValue = "Value";
const idValue = "Value";

var outputInit = "/d/%D0%A0%D0%B0%D0%B7%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D0%BA%D0%B0/%D0%91%D1%83%D0%BC%D0%B0%D0%B3%D0%B0/%D0%9A%D0%BB%D1%8E%D1%87%D0%B8%20%D0%BF%D1%80%D0%B8%D1%87%D0%B8%D0%BD/%D0%A3%D0%BF%D1%80%D0%BE%D1%89%D0%B5%D0%BD%D0%BD%D0%B0%D1%8F/ModelAnalyzer/%D0%93%D1%80%D0%B0%D1%84%D0%B8%D0%BA%D0%B0/%D0%9A%D0%B0%D1%80%D1%82%D1%8B";
var filesPrefix = "card";
var useCircular = true;
var showSystemInfo = false;
var previewMode = false;
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
      var items = relations.child(relationElement);
      var mainGroup = doc.layers.getByName(relationsLayerName).groupItems;
      for (var i = 0; i < mainGroup.length; i++)
      {
            mainGroup.getByName(i).hidden = true;
      }
  
      var minPaired = 0;
      for each(var relation in items)
      {
          var position = relation.child(positionElement).toString();
          var groupItem = mainGroup.getByName(position);
          groupItem.hidden = false;
          var reasonGroup = groupItem.groupItems.getByName(reasonGroupName);
          var blockerGroup = groupItem.groupItems.getByName(blockerGroupName);       
          
          var isReason = relation.child(typeElement).toString() != blockerType
          reasonGroup.hidden = !isReason;
          blockerGroup.hidden = isReason;
          var activeGroup = isReason ? reasonGroup : blockerGroup;
          
          var isBack = relation.child(directionElement).toString() == backDirection;
          var backGroup = activeGroup.pathItems.getByName(backGroupName);
          var frontGroup = activeGroup.pathItems.getByName(frontGroupName);    
          backGroup.hidden = !isBack;
          frontGroup.hidden = isBack;
          
          reasonGroup.pathItems.getByName(pairedGroupName).hidden = true;
          if (relation.child(typeElement).toString() == pairedReasonType)
          {
              minPaired = minPaired < parseInt (position) ? minPaired : parseInt (position);
          }
      }

     if (minPaired != 0) 
     {
         var reasonGroup = mainGroup.groupItems.getByName(minPaired).pathItems.getByName(reasonGroupName);
         reasonGroup.pathItems.getByName(pairedGroupName).hidden = false;
     }
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
    exportOptions.verticalScale = 500;
    exportOptions.horizontalScale = 500;

    var type = ExportType.PNG24;
    var fileSpec = new File(file);

    doc.exportFile(fileSpec, type, exportOptions);
}

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
            ExportFileToPNG24(filePath, doc);
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