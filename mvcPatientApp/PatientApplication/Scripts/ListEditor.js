function ListEditor(id, elementsCount, inputCssClass, removeText, removeClass) {
	this.elementsCount = elementsCount;
	this.list = document.getElementById(id);
	this.inputCssClass = inputCssClass;
	this.removeText = removeText;
	this.removeClass = removeClass;

	if (typeof (listEditorPrototypeCalled) === 'undefined') {
		listEditorPrototypeCalled = true;

		ListEditor.prototype.Remove = function (element) {
			this.list.removeChild(element.parentNode);
			this.elementsCount--;
			this.ReindexChildren();
		}

		ListEditor.prototype.ReindexChildren = function () {
			for (var i = 0; i < this.list.children.length; i++) {
				var itemNode = this.list.children[i];
				if (itemNode.tagName != 'LI') {
					continue;
				}

				var inputText = itemNode.children[0];
				var pos = inputText.name.lastIndexOf('[');
				inputText.name = inputText.name.substring(0, pos) + '[' + i + ']';
			}
		}

		ListEditor.prototype.AddItem = function () {
			var listItem = document.createElement('li');
			var input = document.createElement('input');
			input.id = this.list.id + '_' + this.elementsCount + '_';
			input.name = this.list.attributes['name'].value + '[' + this.elementsCount + ']';
			input.type = 'text';
			input.className = this.inputCssClass;

			var remove = document.createElement('span');
			remove.className = this.removeClass;
			var editorId = this.list.id;
			remove.onclick = function () { listEditors[editorId].Remove(remove) };
			remove.textContent = this.removeText;

			listItem.appendChild(input);
			listItem.appendChild(remove);

			this.list.appendChild(listItem);
			input.focus();
			this.elementsCount++;
		}
	}
}

var listEditors = {};

document.addEventListener('DOMContentLoaded', function () {
	var ulElements = document.getElementsByClassName('ListEditor');
	for (var i = 0; i < ulElements.length; i++) {
		var ul = ulElements[i];

		var listEditor = new ListEditor(ul.id, ul.dataset.elementscount, ul.dataset.inputcssclass, ul.dataset.removetext, ul.dataset.removeclass);
		listEditors[ul.id] = listEditor;
	}
}, false);