var HandleIO = {

	SyncFiles : function()
	{
		FS.syncfs(false,function (err) {
			// handle callback
		});
	},

	HideLoadingScreen : function()
	{
		document.getElementById("loadingPage").style.display = "none";
	},

	ShowFooter : function()
	{
		var footerElem = document.getElementById("footer");
		footerElem.style = '';
	},

	HideFooter : function()
	{
		var footerElem = document.getElementById("footer");
		footerElem.style = 'display: none;';
	}
};

mergeInto(LibraryManager.library, HandleIO);