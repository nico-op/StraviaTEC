import 'package:flutter/material.dart';
import 'package:location/location.dart';
import 'package:path_provider/path_provider.dart';
import 'dart:async';
import 'dart:io';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: const MyHomePage(),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({Key? key}) : super(key: key);

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  bool isRunning = false;
  int hours = 0, minutes = 0, seconds = 0;
  double kilometers = 0.0, velocidadPromedio = 0.0;
  final double velocidad = 2.0; // Velocidad constante para prueba
  late Stopwatch stopwatch;
  Location location = new Location();
  bool _serviceEnabled = false;
  PermissionStatus _permissionGranted = PermissionStatus.denied;
  List<LocationData> route = [];

  @override
  void initState() {
    super.initState();
    stopwatch = Stopwatch();
    initLocationService();
  }

  Future<void> initLocationService() async {
    _serviceEnabled = await location.serviceEnabled();
    if (!_serviceEnabled) {
      _serviceEnabled = await location.requestService();
      if (!_serviceEnabled) return;
    }

    _permissionGranted = await location.hasPermission();
    if (_permissionGranted == PermissionStatus.denied) {
      _permissionGranted = await location.requestPermission();
      if (_permissionGranted != PermissionStatus.granted) return;
    }
  }

  void startTimer() {
    if (!isRunning) {
      stopwatch.start();
      location.onLocationChanged.listen((LocationData currentLocation) {
        // Si el cronómetro está en funcionamiento, agregue la ubicación a la ruta
        if (stopwatch.isRunning) {
          setState(() {
            route.add(currentLocation);
          });
        }
      });

      Timer.periodic(Duration(seconds: 1), (timer) {
        if (stopwatch.isRunning) {
          setState(() {
            seconds = stopwatch.elapsed.inSeconds % 60;
            minutes = (stopwatch.elapsed.inSeconds ~/ 60) % 60;
            hours = stopwatch.elapsed.inSeconds ~/ 3600;
            kilometers = velocidad * stopwatch.elapsed.inSeconds / 1000.0;

            if (stopwatch.elapsed.inSeconds > 0) {
              velocidadPromedio = kilometers / stopwatch.elapsed.inSeconds;
            }
          });
        }
      });
      setState(() {
        isRunning = true;
      });
    }
  }

  void stopTimer() async {
    if (isRunning) {
      stopwatch.stop();
      await saveGPXFile();
      setState(() {
        isRunning = false;
      });
    }
  }

  void resetTimer() {
    stopwatch.reset();
    setState(() {
      isRunning = false;
      hours = 0;
      minutes = 0;
      seconds = 0;
      kilometers = 0.0;
      velocidadPromedio = 0.0;
      route.clear();
    });
  }

  Future<void> saveGPXFile() async {
    final directory = await getApplicationDocumentsDirectory();
    final path = '${directory.path}/route.gpx';
    final file = File(path);

    String gpxContent = generateGPX();
    await file.writeAsString(gpxContent);
    print('GPX file saved at $path');
  }

  String generateGPX() {
    StringBuffer sb = StringBuffer();
    sb.writeln('<?xml version="1.0" encoding="UTF-8"?>');
    sb.writeln('<gpx version="1.1" creator="FlutterApp">');
    sb.writeln('<trk>');
    sb.writeln('<name>RutaDeportiva</name>');
    sb.writeln('<trkseg>');

    for (var point in route) {
      sb.writeln('<trkpt lat="${point.latitude}" lon="${point.longitude}">');
      sb.writeln('<time>${DateTime.now().toIso8601String()}</time>');
      sb.writeln('</trkpt>');
    }

    sb.writeln('</trkseg>');
    sb.writeln('</trk>');
    sb.writeln('</gpx>');

    return sb.toString();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Cronómetro, Distancia y Velocidad Flutter'),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Text(
              'Tiempo: $hours : $minutes : $seconds ',
              style: TextStyle(fontSize: 20),
            ),
            const SizedBox(height: 10),
            Text(
              'Distancia: ${kilometers.toStringAsFixed(2)} km',
              style: TextStyle(fontSize: 20),
            ),
            const SizedBox(height: 10),
            Text(
              'Velocidad Promedio: ${velocidadPromedio.toStringAsFixed(2)} m/s',
              style: TextStyle(fontSize: 20),
            ),
            const SizedBox(height: 20),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: <Widget>[
                ElevatedButton(
                  onPressed: startTimer,
                  child: const Text('Iniciar'),
                ),
                ElevatedButton(
                  onPressed: stopTimer,
                  child: const Text('Detener'),
                ),
                ElevatedButton(
                  onPressed: resetTimer,
                  child: const Text('Reiniciar'),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
