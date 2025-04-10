mergeInto(LibraryManager.library, {
    ShowAdv: function() {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onOpen: function() {
                    myGameInstance.SendMessage('RBAds', 'OnOpen');
                },
                onClose: function(wasShown) {
                    if(wasShown) {
                        myGameInstance.SendMessage('RBAds', 'OnClose', 'Closed');
                    } else {
                        myGameInstance.SendMessage('RBAds', 'OnClose', 'notClosed');
                    }
                },
                onError: function(error) {
                    myGameInstance.SendMessage('RBAds', 'OnError');
                },
                onOffline: function(error) {
                    myGameInstance.SendMessage('RBAds', 'OnOffline');
                }
            }
        });
    }    
});