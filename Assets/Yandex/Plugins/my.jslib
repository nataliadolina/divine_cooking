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

  GetLang : function () {
      var lang = ysdk.environment.i18n.lang;
      var bufferSize = lengthBytesUTF8(lang) + 1;
      var buffer = _malloc(bufferSize);
      stringToUTF8(lang, buffer, bufferSize);
      console.log(buffer);
      return buffer;
  },

});