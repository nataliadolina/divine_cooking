mergeInto(LibraryManager.library, {
  RateGame : function () {
    ysdk.feedback.canReview()
      .then(({value, reason}) => {
        if (value) {
          ysdk.feedback.requestReview()
            .then(({ feedbackSent }) => {
              console.log(feedbackSent);
          })
          } else {
                console.log(reason)
            }
        })
  },

  SaveExtern: function (date) {
    if (player.getMode() !== 'lite'){
      //player is authorized
      var dateString = UTF8ToString(date);
      var myObj = JSON.parse(dateString);
      player.setData(myObj).then(() => console.log("data is set"));
    }
    else{
      console.log("player mode is not authorized");
    }
  },

  LoadExtern: function () {
    if (player.getMode() !== "lite"){
      //player is authorized
      player.getData().then(_date => {
        const myJSON = JSON.stringify(_date);
        myGameInstance.SendMessage("ProjectContext(Clone)", "SetPlayerInfo", myJSON);
      });
    }
    else{
      console.log("player is not authorized");
    }
  },

  CheckAuthorized : function() {
    if (player.getMode() === "lite"){
      myGameInstance.SendMessage("SignInPanel", "SetActiveGameObject");
    }
    else{
      myGameInstance.SendMessage("SignInPanel", "SetInactiveGameObject");
    }
  },

  Auth : function () {
    initPlayer().then(_player => {
        if (_player.getMode() === 'lite') {
            // Player is not authorized
            ysdk.auth.openAuthDialog().then(() => {
                    // player inited successfully
                    initPlayer().then(_player => {
                        // player inited successfully
                      myGameInstance.SendMessage("SignInPanel", "SetInactiveGameObject");
                      LoadExtern();
                    }).catch(err => {
                        // error while Player init
                    });
                }).catch(() => {
                    // player is not authorized
                });
        }
    }).catch(err => {
        // error while player initialization
    });
  },

  GetLang : function () {
      var lang = ysdk.environment.i18n.lang;
      var bufferSize = lengthBytesUTF8(lang) + 1;
      var buffer = _malloc(bufferSize);
      stringToUTF8(lang, buffer, bufferSize);
      return buffer;
  },

  GetPlatform : function () {
    var platform = ysdk.deviceInfo.type;
    var bufferSize = lengthBytesUTF8(platform) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(platform, buffer, bufferSize);
    return buffer;
  },

  ShowAdv : function () {
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          myGameInstance.SendMessage("Sounds", "UnPauseMusic");
          myGameInstance.SendMessage("GameManager", "StartGame");
        },
        onError: function(error) {
          myGameInstance.SendMessage("Sounds", "UnPauseMusic");
          myGameInstance.SendMessage("GameManager", "StartGame");
        }
      }
    })
  },

});