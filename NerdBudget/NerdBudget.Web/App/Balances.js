/*
  1. THIS TEMPLATE IS VERY TIGHTLY BOUND TO NERDBUDGET'S DESIGN
  2. THIS NEEDS TO BE IN THE APPROPRIATE CONTROLLER DIRECTORY!!
  3. THIS TEMPLATE AND ASSOCIATED JAVASCRIPT FILE USE THE
	App/Helper.js FILE FROM NERDBUDGET
*/
'use strict';

function BalanceListViewModel(data)
{
	var self = this;

	self.url = 'api/Balances';

	self.balances = ko.utils.arrayMap(data.balances, function (x) { ko.track(x); return x; });

	self.update = update;

	ko.track(self);

	function getFormElement(disableIt)
	{
		var html = $('#form-body').html();

		var $html = $(html);

		$html.submit(function () { return false; });

		if (disableIt)
		{
			$html.disableAll();
		}

		return $html.get(0);
	};

	function update(balance)
	{
		var element = getFormElement();

		var vm = new BalanceDetailModel(balance);

		ko.applyBindings(vm, element);

		var options = nbHelper.crudDialog('update', element);

		options.buttons.ok.callback = function ()
		{
			var dlg = this;

			var onSuccess = function (data)
			{
				nbHelper.overlay(data, balance);

				ko.cleanNode(element);

				dlg.modal('hide');
			};

			var onError = function (error)
			{
				dlg.find('form').showErrors(error);
			};

			$.update(self.url + '/{accountId}/{asOf}', vm.getData()).then(onSuccess, onError);

			return false;
		};

		options.buttons.cancel.callback = function () { ko.cleanNode(element); };

		bootbox.dialog(options);
	};

};

function BalanceDetailModel(balance)
{
	var self = this;
	
	self.accountId = '';
	self.asOf = '';
	self.amount = '';

	self.update = update;

	self.getData = getData;

	self.update(balance);

	ko.track(self, ['accountId', 'asOf', 'amount']);

	function update(data)
	{
		if (data)
		{ 
			self.accountId = data.accountId || '';
			self.asOf = data.asOf || '';
			self.amount = data.amount || '';
		}
	};

	function getData()
	{
		return { 
			accountId: self.accountId,
			asOf: moment(self.asOf).format('YYYY-MM-DD'),
			amount: self.amount
		};
	}
};