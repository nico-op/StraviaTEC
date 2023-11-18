import 'package:flutter/material.dart';
import 'package:geolocator/geolocator.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:movil/ruta/manejo_gpx.dart';
import 'package:permission_handler/permission_handler.dart';

class MapWidget extends StatefulWidget {
  final Function(List<LatLng>) onRouteUpdated;

  const MapWidget({Key? key, required this.onRouteUpdated}) : super(key: key);

  // Modifica esta función para transformar LatLng de latlong a google_maps_flutter
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
  GPXHelper gpxHelper = GPXHelper();

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

  Future<void> _saveRouteToGPX() async {
    List<Coordenadas> coordenadas = widget.transformarPuntos(_routePoints);
    await gpxHelper.saveGPXToFile(coordenadas);
    print('Ruta guardada');
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
              _saveRouteToGPX(); // Llama al método aquí
              print('Ruta guardada');
            },
            child: Text('Guardar Ruta'),
          ),
        ],
      ),
    );
  }
}
