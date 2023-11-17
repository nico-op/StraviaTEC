import 'package:flutter/material.dart';
import 'package:geolocator/geolocator.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:permission_handler/permission_handler.dart';

class MapWidget extends StatefulWidget {
  const MapWidget({Key? key}) : super(key: key);

  @override
  _MapWidgetState createState() => _MapWidgetState();
}

class _MapWidgetState extends State<MapWidget> {
  late GoogleMapController mapController;
  LatLng? _currentPosition;
  Set<Marker> _markers = {}; // Conjunto de marcadores

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
          _addMarker(
              _currentPosition!); // Agregar marcador con la ubicación actual
        });
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
      // Si hay un error al obtener la ubicación, _currentPosition permanece como null
    }
  }

  // Método para agregar un marcador en una ubicación dada
  void _addMarker(LatLng position) {
    _markers.add(
      Marker(
        markerId: MarkerId('userLocation'),
        position: position,
        icon: BitmapDescriptor.defaultMarkerWithHue(
            BitmapDescriptor.hueRed), // Icono rojo
        infoWindow:
            InfoWindow(title: 'Tu ubicación'), // Información del marcador
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return GoogleMap(
      onMapCreated: (GoogleMapController controller) {
        mapController = controller;
        if (_currentPosition != null) {
          mapController.animateCamera(
            CameraUpdate.newCameraPosition(
              CameraPosition(target: _currentPosition!, zoom: 12),
            ),
          );
        }
      },
      initialCameraPosition: CameraPosition(
        target:
            _currentPosition ?? LatLng(9.935379777985046, -84.1047670168582),
        zoom: 12,
      ),
      markers: _markers, // Conjunto de marcadores a mostrar en el mapa
    );
  }
}
