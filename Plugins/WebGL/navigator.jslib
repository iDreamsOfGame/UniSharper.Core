mergeInto(LibraryManager.library, {
	GetUserAgent: function () {
		var userAgent = navigator.userAgent;
		var bufferSize = lengthBytesUTF8(userAgent) + 1;
		var buffer = _malloc(bufferSize);
    	stringToUTF8(userAgent, buffer, bufferSize);
    	return buffer;
	},

	IsMobilePlatform: function() {
        var userAgent = navigator.userAgent;
        isMobile = (
                    /\b(BlackBerry|webOS|iPhone|IEMobile)\b/i.test(userAgent) ||
                    /\b(Android|Windows Phone|iPad|iPod)\b/i.test(userAgent) ||
                    // iPad on iOS 13 detection
                    (userAgent.includes("Mac") && "ontouchend" in document)
                );
        return isMobile;
    }
});