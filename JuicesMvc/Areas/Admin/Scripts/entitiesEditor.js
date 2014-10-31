// depends on $$.ui, knockout

(function (editor, $, undefined) {
	editor.ModeEnum = {
		VIEW: 0,
		EDIT: 1
	};

	editor.BaseEntityViewModel = function (options) {
		var self = this;

		var DEFAULT_SETTINGS = {
			customDtoMapping: null
		};

		options = $.extend(DEFAULT_SETTINGS, options || {});

		self.mode = ko.observable(editor.ModeEnum.VIEW);

		self.edit = function () { self.mode(editor.ModeEnum.EDIT); };
		self.cancelEdit = function () {
			self.modelState({});
			self.mode(editor.ModeEnum.VIEW);
			this.resetChanges();
		};

		self.acceptChanges = function () {
			if (!(this.dtoProps in window)) {
				for (var i = 0; i < this.dtoProps.length; i++) {
					var propName = this.dtoProps[i];
					this[propName].commit();
				}
				self.modelState({});
				self.mode(editor.ModeEnum.VIEW);
			} else logMissingProps();
		};

		self.resetChanges = function () {
			if (!(this.dtoProps in window)) {
				for (var i = 0; i < this.dtoProps.length; i++) {
					var propName = this.dtoProps[i];
					this[propName].reset();
				}
			} else logMissingProps();
		};

		function isNewEntity(entity) {
			return entity.Id() === -1;
		}

		function logMissingProps() {
			console.log("Can't find info about props");
		}

		self.modelState = ko.observable({});

		self.update = function (updateAction) {
			var entity = this;
			$.post(updateAction, entity.getDto(), function (resp) {
				if ($.isNumeric(resp)) {
					entity.Id(resp);
					entity.acceptChanges();
					return;
				} else if (resp.Success !== undefined) {
					if (resp.Success === true) {
						entity.acceptChanges();
						return;
					} else if (resp.ModelErrors !== undefined) {
						self.modelState(JSON.parse(resp.ModelErrors));
					} else console.log(resp.CustomError);
				} else console.log(resp);
				if (!isNewEntity(entity)) entity.resetChanges();
			});
		};

		self.delete = function (deleteAction, deleted) {
			$.get(deleteAction, { Id: this.Id() }, function (resp) {
				if (resp === true)
					deleted();
				else console.log(resp);
			});
		};

		self.getDto = function () {
			if (!(this.dtoProps in window)) {
				var dto = { Id: this.Id() };

				for (var i = 0; i < this.dtoProps.length; i++) {
					var propName = this.dtoProps[i];

					if (options.customDtoMapping) {
						if (propName in options.customDtoMapping) {
							dto[propName] = options.customDtoMapping[propName](this[propName].getTempValue());
							continue;
						}
					}

					dto[propName] = this[propName].getTempValue();
				}

				return dto;
			}
			logMissingProps();
			return null;
		};

		self.template = ko.computed(function () {
			switch (self.mode()) {
				case editor.ModeEnum.VIEW:
					return "View";
				case editor.ModeEnum.EDIT:
					return "Edit";
			}

			return "View";
		});
	};

	editor.EntityListViewModel = function (actions, entities, defaultEntity, entityOptions) {
		var self = this;

		editor.basicMapping = {
			create: function (options) {
				var obj = {};
				obj.dtoProps = [];
				for (var propName in options.data) {
					if (propName == 'Id') obj[propName] = ko.observable(options.data[propName]);
					else if (options.data[propName] instanceof Array) {
						obj[propName] = ko.protectedArrayObservable(options.data[propName]);
						obj.dtoProps.push(propName);
					} else {
						obj[propName] = ko.protectedObservable(options.data[propName]);
						obj.dtoProps.push(propName);
					}
				}
				return $.extend(obj, new editor.BaseEntityViewModel(entityOptions));
			}
		};

		self.entities = ko.mapping.fromJS(entities, editor.basicMapping);

		self.save = function () {
			this.update(actions.update);
		};

		self.delete = function () {
			var entity = this;
			$$.ui.confirm("Вы подтверждаете удаление?", function () {
				if (entity.Id() !== -1) {
					entity.delete(actions.delete, function () {
						self.entities.remove(entity);
					});
				}
				self.entities.remove(entity);
			});
		};

		self.add = function () {
			var entityViewModel = ko.mapping.fromJS(defaultEntity, editor.basicMapping);
			entityViewModel.edit();
			self.entities.push(entityViewModel);
		};
	};

})($$.ui.editor = $$.ui.editor || {}, jQuery);
//TODO: find a better way for orginizing javascript modules