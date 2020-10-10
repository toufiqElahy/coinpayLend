var ethers = require('ethers');

var ContractAddress = "0xCA6d6989519E2e0f32b6013Cff0dF2B874254353";//"0xCeE502d2fb3C3ab8a2D30eD7b8863A488d3eaA7D";

var abi = [{ "constant": true, "inputs": [], "name": "mainAddress", "outputs": [{ "name": "", "type": "address" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "totalFees", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_paused", "type": "bool" }], "name": "setPaused", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [{ "name": "", "type": "address" }], "name": "userList", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "allowSponsorChange", "outputs": [{ "name": "", "type": "bool" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "total", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_userID", "type": "uint256" }, { "name": "_wallet", "type": "address" }, { "name": "_referrerID", "type": "uint256" }, { "name": "_introducerID", "type": "uint256" }, { "name": "_referral1", "type": "address" }, { "name": "_referral2", "type": "address" }, { "name": "_referral3", "type": "address" }, { "name": "star", "type": "uint256" }], "name": "setUserData", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [{ "name": "", "type": "uint256" }], "name": "users", "outputs": [{ "name": "isExist", "type": "bool" }, { "name": "wallet", "type": "address" }, { "name": "referrerID", "type": "uint256" }, { "name": "introducerID", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [{ "name": "", "type": "uint256" }], "name": "STAR_FEE", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "paused", "outputs": [{ "name": "", "type": "bool" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_star", "type": "uint256" }, { "name": "_price", "type": "uint256" }], "name": "setStarFee", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [{ "name": "_user", "type": "uint256" }, { "name": "_star", "type": "uint256" }], "name": "viewUserStarActive", "outputs": [{ "name": "", "type": "bool" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [{ "name": "", "type": "uint256" }], "name": "STAR_PRICE", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "currentUserID", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": true, "inputs": [], "name": "tempReferrerID", "outputs": [{ "name": "", "type": "uint256" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_id", "type": "uint256" }], "name": "changeReferrerID", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": false, "inputs": [{ "name": "_allowSponsorChange", "type": "bool" }], "name": "setAllowSponsorChange", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [], "name": "deployer", "outputs": [{ "name": "", "type": "address" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_currentUserID", "type": "uint256" }], "name": "setCurrentUserID", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": false, "inputs": [{ "name": "_mainAddress", "type": "address" }], "name": "setMainAddress", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [{ "name": "_user", "type": "uint256" }], "name": "viewUserReferrals", "outputs": [{ "name": "", "type": "address[]" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_star", "type": "uint256" }, { "name": "_price", "type": "uint256" }], "name": "setStarPrice", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "constant": true, "inputs": [{ "name": "_user", "type": "uint256" }], "name": "findFreeReferrer", "outputs": [{ "name": "", "type": "address" }], "payable": false, "stateMutability": "view", "type": "function" }, { "constant": false, "inputs": [{ "name": "_owner", "type": "address" }], "name": "transferOwnership", "outputs": [], "payable": false, "stateMutability": "nonpayable", "type": "function" }, { "inputs": [], "payable": false, "stateMutability": "nonpayable", "type": "constructor" }, { "payable": true, "stateMutability": "payable", "type": "fallback" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_user", "type": "uint256" }, { "indexed": true, "name": "_referrer", "type": "uint256" }, { "indexed": true, "name": "_introducer", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "Register", "type": "event" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_user", "type": "uint256" }, { "indexed": true, "name": "_referrer", "type": "uint256" }, { "indexed": true, "name": "_introducer", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "SponsorChange", "type": "event" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_user", "type": "uint256" }, { "indexed": false, "name": "_star", "type": "uint256" }, { "indexed": false, "name": "_price", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "Upgrade", "type": "event" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_user", "type": "uint256" }, { "indexed": true, "name": "_referrer", "type": "uint256" }, { "indexed": true, "name": "_introducer", "type": "uint256" }, { "indexed": false, "name": "_star", "type": "uint256" }, { "indexed": false, "name": "_money", "type": "uint256" }, { "indexed": false, "name": "_fee", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "Payment", "type": "event" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_referrer", "type": "uint256" }, { "indexed": true, "name": "_referral", "type": "uint256" }, { "indexed": false, "name": "_star", "type": "uint256" }, { "indexed": false, "name": "_money", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "LostMoney", "type": "event" }, { "anonymous": false, "inputs": [{ "indexed": true, "name": "_referrer", "type": "uint256" }, { "indexed": true, "name": "_referral", "type": "uint256" }, { "indexed": false, "name": "_star", "type": "uint256" }, { "indexed": false, "name": "_money", "type": "uint256" }, { "indexed": false, "name": "_time", "type": "uint256" }], "name": "IntroducerLostMoney", "type": "event" }];

var provider = ethers.getDefaultProvider();

var contract = new ethers.Contract(ContractAddress, abi, provider);

var etherscanTx = 'https://etherscan.io/tx/';


async function getStarPrice(lvl) {
	var price = await contract.STAR_PRICE(lvl);
	let wei = ethers.utils.bigNumberify(price.toString());

	return ethers.utils.formatEther(wei);
}
async function getIdbyAddress(addr) {
	var id = await contract.userList(addr);
	return id.toString();
}

async function starLevel(id) {
	var star = 2;
	for (var i = 2; i <= 6; i++) {
		var bol = await contract.viewUserStarActive(id, i);
		console.log(bol);
		if (bol == false) {

			break;
		}
		star++;
	}

	return star;
}

async function idsByReferrals(addresses) {

	var ids = [];
	for (var i = 0; i < addresses.length; i++) {
		ids.push(await getIdbyAddress(addresses[i]));
	}
	//console.log(ids);
	return ids;
}

module.exports.getId = async function (callback, getAddress) {
	var id = await getIdbyAddress(getAddress);
	callback(null, id);
};

module.exports.getPendingTx = async function (callback, getAddress) {
	var transactionCountLatest = await provider.getTransactionCount(getAddress);
	var transactionCount = await provider.getTransactionCount(getAddress,'pending');
	callback(null, transactionCount == transactionCountLatest);
};

module.exports.balance = async function (callback, getAddress) {
	var starPrce = 0;
	var star = '1';
	var id = await getIdbyAddress(getAddress);
	if (id != '0') {
		star = await starLevel(id);
	}
	starPrce = await getStarPrice(star);

	var val = await provider.getBalance(getAddress);
	val = parseInt(val.toString()) / 10 ** 18;

	if (star == '7') {
		callback(null, val + " Already Bought all levels");
	}
	callback(null, val + " Buy Level " + star + " =" + starPrce + " ETH");
};

module.exports.connect = async function (callback, getAddress,refId) {
	var referrer = '0x';
	var starPrce = 0;

	var id = await getIdbyAddress(getAddress);
	if (id == '0') {
		//var tempId = await contract.tempReferrerID();
		var user = await contract.users(refId);//user.wallet
		referrer = user.wallet;
		starPrce = await getStarPrice('1');
	} else {
		var star = await starLevel(id);
		starPrce = await getStarPrice(star);
		if (star == '7') {
			callback(null, " Already Bought all levels");
		}
	}

	var val = await provider.getBalance(getAddress);
	val = parseInt(val.toString()) / 10 ** 18;
	if (starPrce > val) {
		callback(null, 'Require ' + starPrce + " ETH but u have " + val + " ETH");
	} else {
		callback(null, referrer + ':' + starPrce);
	}


	//var walletWithProvider = new ethers.Wallet(privateKey, provider);

	//var transaction = {
	//	to: ContractAddress,
	//	value: ethers.utils.parseEther(starPrce),//'0x00',//
	//	data: referrer
	//}


	//walletWithProvider.sendTransaction(transaction).then((txd) => {

	//	callback(null, etherscanTx + txd.hash);

	//});
};

