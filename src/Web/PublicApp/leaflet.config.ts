import {LatLngExpression, TileLayerOptions} from "leaflet";

export const streetMap: TileLayerOptions = {
	attribution:
		'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
	maxZoom: 18,
	minZoom: 6,
	id: "mapbox/streets-v11",
	tileSize: 512,
	zoomOffset: -1,
	accessToken: "pk.eyJ1Ijoic2ltb25uaWVzZSIsImEiOiJja3ZnZG9yZm0wMWxzMnVwM3Nlcjk3YmpiIn0.p1TDwifWqx1kcYFnBqI_fg",
	bounds: [
		[57.765923, 7.482181],
		[54.54649, 15.455954],
	],
};

export const satelliteMap: TileLayerOptions = {
	attribution:
		'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
	maxZoom: 18,
	minZoom: 6,
	id: "mapbox/satellite-streets-v11",
	tileSize: 512,
	zoomOffset: -1,
	accessToken: "pk.eyJ1Ijoic2ltb25uaWVzZSIsImEiOiJja3ZnZG9yZm0wMWxzMnVwM3Nlcjk3YmpiIn0.p1TDwifWqx1kcYFnBqI_fg",
	bounds: [
		[57.765923, 7.482181],
		[54.54649, 15.455954],
	],
};

export const boundariesCoords = [
	[
		[
			[54.783554, 8.025203],
			[56.7008, 7.94616],
			[57.209392, 8.586408],
			[57.286365, 9.297795],
			[57.950948, 10.293736],
			[57.799633, 11.31339],
			[56.031885, 12.661413],
			[55.803497, 12.838052],
			[55.50243, 12.822007],
			[55.324403, 12.712139],
			[54.5551, 12.744531],
			[54.506417, 11.522005],
			[54.805709, 9.818059],
			[54.783554, 8.025203],
		],
	],
	[
		[
			[55.319725, 14.763668],
			[55.145379, 15.20082],
			[54.962571, 15.098907],
			[55.096303, 14.630914],
			[55.319725, 14.763668],
		],
	],
];

export const greyOutCoords: LatLngExpression[][] = [
	[
		[90, 180],
		[90, -180],
		[-90, -180],
		[-90, 180],
		[90, 180]
	],
	[
		[54.783554, 8.025203],
		[56.7008, 7.94616],
		[57.209392, 8.586408],
		[57.286365, 9.297795],
		[57.950948, 10.293736],
		[57.799633, 11.31339],
		[56.031885, 12.661413],
		[55.803497, 12.838052],
		[55.50243, 12.822007],
		[55.324403, 12.712139],
		[54.5551, 12.744531],
		[54.506417, 11.522005],
		[54.805709, 9.818059],
		[54.783554, 8.025203]
	],
	[
		[55.319725, 14.763668],
		[55.145379, 15.20082],
		[54.962571, 15.098907],
		[55.096303, 14.630914],
		[55.319725, 14.763668]
	]
];
