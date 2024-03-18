mergeInto(LibraryManager.library, {
  GetPlayerData: function () {
    myGameInstance.SendMessage('Yandex', 'SetName', player.getName());
    myGameInstance.SendMessage('Yandex', 'SetPhoto', player.getPhoto("medium"));
  },

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
    console.log(date);
    var dateString = UTF8ToString(date);
    var myObj = JSON.parse(dateString);
    player.setData(myObj).then(() => console.log("data is set"));
  },

  LoadExtern: function () {
    player.getData().then(_date => {
      const myJSON = JSON.stringify(_date);
      myGameInstance.SendMessage("ProjectContext(Clone)", "SetPlayerInfo", myJSON);
    });
      
  },
});