import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_polyline_points/flutter_polyline_points.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

class RouteMapWidget extends StatefulWidget {
  final List<LatLng> routePoints;

  const RouteMapWidget({Key? key, required this.routePoints}) : super(key: key);

  @override
  _RouteMapWidgetState createState() => _RouteMapWidgetState();
}

class _RouteMapWidgetState extends State<RouteMapWidget> {
  late GoogleMapController mapController;
  List<LatLng> polylineCoordinates = [];
  PolylinePoints polylinePoints = PolylinePoints();

  void getPolyPoints() async {
    PolylineResult result = await polylinePoints.getRouteBetweenCoordinates(
      'AIzaSyBsTE308gk72x1gSPonOkcp1FroLVMjJr8',
      PointLatLng(widget.routePoints.first.latitude,
          widget.routePoints.first.longitude),
      PointLatLng(
          widget.routePoints.last.latitude, widget.routePoints.last.longitude),
    );

    if (result.points.isNotEmpty) {
      setState(() {
        polylineCoordinates = result.points
            .map((point) => LatLng(point.latitude, point.longitude))
            .toList();
      });
    }
  }

  Future<void> saveRouteToGPX() async {
    try {
      // Generar contenido GPX
      String gpxContent = generateGPXContent(polylineCoordinates);

      // Guardar archivo GPX
      File file = File(
          'ruta_trazada.gpx'); // Puedes cambiar el nombre del archivo si lo deseas
      await file.writeAsString(gpxContent);

      print('Archivo GPX guardado en: ${file.path}');
    } catch (e) {
      print('Error guardando archivo GPX: $e');
    }
  }

  String generateGPXContent(List<LatLng> routePoints) {
    final StringBuffer gpxContent = StringBuffer();

    gpxContent
        .writeln('<?xml version="1.0" encoding="UTF-8" standalone="no" ?>');
    gpxContent.writeln(
        '<gpx version="1.1" xmlns="http://www.topografix.com/GPX/1/1">');

    for (LatLng point in routePoints) {
      gpxContent
          .writeln('  <wpt lat="${point.latitude}" lon="${point.longitude}">');
      gpxContent.writeln('    <name>Waypoint</name>');
      gpxContent.writeln('    <desc>Details about the waypoint</desc>');
      gpxContent.writeln('  </wpt>');
    }

    gpxContent.writeln('</gpx>');

    return gpxContent.toString();
  }

  @override
  void initState() {
    super.initState();
    getPolyPoints();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Ruta en el mapa'),
        actions: [
          IconButton(
            icon: Icon(Icons.save),
            onPressed: () {
              saveRouteToGPX();
            },
          ),
        ],
      ),
      body: GoogleMap(
        onMapCreated: (GoogleMapController controller) {
          mapController = controller;
        },
        initialCameraPosition: CameraPosition(
          target: widget.routePoints.first,
          zoom: 12,
        ),
        polylines: {
          Polyline(
            polylineId: PolylineId('route'),
            color: Colors.blue,
            points: polylineCoordinates,
            width: 3,
          ),
        },
      ),
    );
  }
}
