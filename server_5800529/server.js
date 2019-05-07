var io = require('socket.io')(process.env.PORT || 3000);

console.log("serverStarted");

var playarcount = 0;
var player = [];
var randomNumber = 0;

function getRandom(min, max) {
    var randN = Math.floor(Math.random() * (+max - +min)) + +min; 
    console.log(randN);
    return randN;
  }

io.on("connection", function (socket) {

    console.log("clientConnected");
    playarcount++;
    var currentPlayer ={};
    if(playarcount==1){
        randomNumber = getRandom(0,99);
    }
    socket.on("guess", function (data) {
        console.log("guessing",JSON.stringify(data));
        currentPlayer = {
            name:data.name,
            gnumber:data.gnumber
        };
        if(currentPlayer.gnumber === randomNumber){
            socket.broadcast.emit("ans",currentPlayer.name);
            console.log(currentPlayer.name+" WIN!!")
            randomNumber = getRandom(0,99);
        }
        if(currentPlayer.gnumber > randomNumber){
            socket.emit("much")
            console.log(currentPlayer.name+" guess too much")
        }
        if(currentPlayer.gnumber < randomNumber){
            socket.emit("less")
            console.log(currentPlayer.name+" guess less")
        }

    })

    socket.on("disconnect", function () {
        console.log("client disconnect");
        playarcount--;
    })
});