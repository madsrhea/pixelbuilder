const canvas = document.getElementById("drawing-screen");
const gridButton = document.getElementById("grid-button");
var widthHeight = 600;
var pixelSize = 15;
var canvasPixelCount = 50;
var canvasArray = new Array(canvasPixelCount);
var currentColor = "#010600";

for (var i = 0; i <= canvasPixelCount; i++)
{
    canvas[i] = new Array(canvasPixelCount);
}

var context = canvas.getContext("2d");

var widthPadding = 635;
var heightPadding = 115;

canvas.height = widthHeight;
canvas.width = widthHeight;

let x = null,
    y = null;

let draw = false;


function GridOnOff()
{
    context.strokeStyle = "#D3D3D3";
    var v = 0;

    for (var h = 0; h <= canvas.width; h += pixelSize)
    {
        for (var v = 0; v <= canvas.height; v += pixelSize) {
            //horizonal lines

            context.moveTo(h, v);
            context.lineTo(canvas.width, v);
            context.lineWidth = .5;
            context.stroke();

            // vertical lines
            context.moveTo(h, v);
            context.lineTo(h, canvas.height);
            context.lineWidth = .5;
            context.stroke();
        }
        
    }
}

context.strokeStyle = "#010600";

window.addEventListener('mousedown', (e) => (draw = true));
window.addEventListener('mouseup', (e) => (draw = false));

window.addEventListener('mousemove', (e) => {
    if (x == null || y == null || !draw) {
        x = e.clientX - widthPadding;
        y = e.clientY - heightPadding;

        return;
    }

    let currentX = e.clientX - widthPadding,
        currentY = e.clientY - heightPadding;

    context.beginPath();
    context.moveTo(x, y);

    for (var i = 0; i < canvas.width; i += pixelSize)
    {
        for (var j = 0; j < canvas.height; j += pixelSize)
        {
            if (currentX < i && currentX > i - pixelSize &&
                currentY < j && currentY > j - pixelSize)
            {
                context.fillRect(i, j, pixelSize, pixelSize);
                context.stroke();
                canvasArray[i / pixelSize, j / pixelSize] = currentColor;
                console.log(canvasArray[i, j] + ", " + i / pixelSize + ", " + j / pixelSize);
            }
        }
    }

    x = currentX;
    y = currentY;

});

var colorList = document.getElementById("all-colors");

function ChangeCurrentColor()
{
    var clicked = $(this).val();
    context.strokeStyle = clicked;
    console.log($(this).val() + " recieved!");

}