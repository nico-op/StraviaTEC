import 'package:flutter/material.dart';
import 'package:flutter_polyline_points/flutter_polyline_points.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

class RouteMapWidget extends StatefulWidget {
  final List<LatLng> routePoints;
  const RouteMapWidget({super.key, required this.routePoints});
  @override
  _RouteMapWidgetState createState() => _RouteMapWidgetState();
}

class _RouteMapWidgetState extends State<RouteMapWidget> {
  late GoogleMapController mapController;
  List<LatLng> polylineCoordinates = [];
  PolylinePoints polylinePoints = PolylinePoints();

  LatLng sourceLocation = LatLng(37.7749, -122.4194); // Coordenadas de inicio
  LatLng destination = LatLng(37.3352, -122.0496); // Coordenadas de fin

  void getPolyPoints() async {
    PolylineResult result = await polylinePoints.getRouteBetweenCoordinates(
      'AIzaSyBsTE308gk72x1gSPonOkcp1FroLVMjJr8',
      PointLatLng(sourceLocation.latitude, sourceLocation.longitude),
      PointLatLng(destination.latitude, destination.longitude),
    );

    if (result.points.isNotEmpty) {
      result.points.forEach((PointLatLng point) {
        polylineCoordinates.add(LatLng(point.latitude, point.longitude));
      });

      setState(() {});
    }
  }

  void navigateToMapWidget(List<LatLng> routePoints) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => RouteMapWidget(routePoints: routePoints),
      ),
    );
  }

  @override
  void initState() {
    super.initState();
    polylineCoordinates = widget.routePoints;
    getPolyPoints();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Ruta en el mapa'),
      ),
      body: GoogleMap(
        onMapCreated: (GoogleMapController controller) {
          mapController = controller;
        },
        initialCameraPosition: CameraPosition(
          target: sourceLocation,
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
