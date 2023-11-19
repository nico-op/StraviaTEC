import 'dart:io';
import 'package:path_provider/path_provider.dart'; // Importa este paquete
import 'package:xml/xml.dart' as xml;
import 'package:flutter/material.dart';
import 'package:geolocator/geolocator.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:permission_handler/permission_handler.dart';

abstract class Coordenadas {
  double get latitude;
  double get longitude;
}

class GoogleMapsLatLng implements Coordenadas {
  final double latitude;
  final double longitude;

  GoogleMapsLatLng(this.latitude, this.longitude);
}

class LatLongLatLng implements Coordenadas {
  final double latitude;
  final double longitude;

  LatLongLatLng(this.latitude, this.longitude);
}

class KMLHelper {
  String generateKMLContent(List<Coordenadas> routePoints) {
    final xml.XmlBuilder kmlBuilder = xml.XmlBuilder();

    kmlBuilder.processing(
        'xml', 'version="1.0" encoding="UTF-8" standalone="no"');
    kmlBuilder.element('kml', namespaces: {
      'xmlns': 'http://www.opengis.net/kml/2.2',
    }, nest: () {
      kmlBuilder.element('Document', nest: () {
        for (Coordenadas point in routePoints) {
          kmlBuilder.element('Placemark', nest: () {
            kmlBuilder.element('Point', nest: () {
              kmlBuilder.element('coordinates',
                  nest: '${point.longitude},${point.latitude}');
            });
          });
        }
      });
    });

    return kmlBuilder.buildDocument().toXmlString(pretty: true, indent: '  ');
  }

  Future<void> saveKMLToFile(List<Coordenadas> routePoints,
      {String fileName = 'straviaTEC_route.kml'}) async {
    String kmlContent = generateKMLContent(routePoints);

    try {
      // Obtiene el directorio de documentos del usuario
      Directory documentsDirectory = await getApplicationDocumentsDirectory();

      // Construye la ruta completa del archivo KML
      String filePath = '${documentsDirectory.path}/$fileName';

      // Guarda el archivo KML en la carpeta de documentos
      File file = File(filePath);
      await file.writeAsString(kmlContent);

      print('KML file saved to: $filePath');
    } catch (e) {
      print('Error saving KML file: $e');
    }
  }
}

class MapWidget extends StatefulWidget {
  final Function(List<LatLng>) onRouteUpdated;

  const MapWidget({Key? key, required this.onRouteUpdated}) : super(key: key);

  List<Coordenadas> transformarPuntos(List<LatLng> puntos) {
    return puntos
        .map((latLng) => GoogleMapsLatLng(latLng.latitude, latLng.longitude))
        .toList();
  }

  @override
  _MapWidgetState createState() => _MapWidgetState();
}

class _MapWidgetState extends State<MapWidget> {
  late GoogleMapController mapController;
  LatLng? _currentPosition;
  Set<Marker> _markers = {};
  List<LatLng> _routePoints = [];
  KMLHelper kmlHelper = KMLHelper();

  @override
  void initState() {
    super.initState();
    _getCurrentLocation();
  }

  Future<void> _getCurrentLocation() async {
    try {
      PermissionStatus status = await Permission.location.request();
      if (status.isGranted) {
        Position position = await Geolocator.getCurrentPosition(
          desiredAccuracy: LocationAccuracy.high,
        );

        setState(() {
          _currentPosition = LatLng(position.latitude, position.longitude);
          _addMarker(_currentPosition!);
          _addRoutePoint(_currentPosition!);
        });

        mapController.animateCamera(
          CameraUpdate.newCameraPosition(
            CameraPosition(target: _currentPosition!, zoom: 12),
          ),
        );
      } else {
        showDialog(
          context: context,
          builder: (BuildContext context) {
            return AlertDialog(
              title: Text('Location Permission'),
              content: Text('This app needs location permission to function.'),
              actions: <Widget>[
                TextButton(
                  child: Text('OK'),
                  onPressed: () {
                    Navigator.of(context).pop();
                  },
                ),
              ],
            );
          },
        );
      }
    } catch (e) {
      print("Error: $e");
    }
  }

  void _addMarker(LatLng position) {
    setState(() {
      _markers.add(
        Marker(
          markerId: MarkerId('userLocation'),
          position: position,
          icon: BitmapDescriptor.defaultMarkerWithHue(BitmapDescriptor.hueBlue),
          infoWindow: InfoWindow(title: 'Tu ubicación'),
        ),
      );
    });
  }

  void _addRoutePoint(LatLng position) {
    setState(() {
      _routePoints.add(position);
      widget.onRouteUpdated(_routePoints);
    });
  }

  Future<void> _saveRouteToKML() async {
    List<Coordenadas> coordenadas = widget.transformarPuntos(_routePoints);
    await kmlHelper.saveKMLToFile(coordenadas);
    print('Ruta guardada como KML');
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Map Widget'),
      ),
      body: Column(
        children: [
          Expanded(
            child: GoogleMap(
              onMapCreated: (GoogleMapController controller) {
                mapController = controller;
              },
              initialCameraPosition: CameraPosition(
                target: _currentPosition ??
                    LatLng(9.935379777985046, -84.1047670168582),
                zoom: 12,
              ),
              markers: _markers,
              polylines: {
                Polyline(
                  polylineId: PolylineId('route'),
                  color: Colors.blue,
                  points: _routePoints,
                ),
              },
            ),
          ),
          ElevatedButton(
            onPressed: () {
              _saveRouteToKML(); // Llama al método aquí
              print('Ruta guardada como KML');
            },
            child: Text('Guardar Ruta como KML'),
          ),
        ],
      ),
    );
  }
}
