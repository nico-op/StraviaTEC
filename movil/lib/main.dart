import 'package:flutter/material.dart';
import 'dart:async';

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
  late Timer timer;

  @override
  void initState() {
    super.initState();
    stopwatch = Stopwatch();
  }

  void _updateTimer(Timer timer) {
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
  }

  void startTimer() {
    if (!isRunning) {
      stopwatch.start();
      timer = Timer.periodic(Duration(seconds: 1), _updateTimer);
      setState(() {
        isRunning = true;
      });
    }
  }

  void stopTimer() {
    if (isRunning) {
      stopwatch.stop();
      timer.cancel();
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
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('StraviaTEC'),
      ),
      body: Column(
        children: [
          // Aquí colocas tu widget de pantalla de mapa con un tamaño específico
          Expanded(
            child: Container(
              color:
                  Colors.blue, // Puedes personalizar esto según tus necesidades
              // Aquí agregarías la lógica para mostrar el mapa
              child: Center(
                child: Text(
                  'mapa',
                  style: TextStyle(fontSize: 24, color: Colors.white),
                ),
              ),
            ),
          ),
          // Textos
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              children: <Widget>[
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: <Widget>[
                      Text(
                        'Tiempo: $hours : $minutes : $seconds ',
                        style: TextStyle(fontSize: 20),
                      ),
                      Text(
                        'Distancia: ${kilometers.toStringAsFixed(2)} km',
                        style: TextStyle(fontSize: 20),
                      ),
                      Text(
                        'Velocidad: ${velocidadPromedio.toStringAsFixed(2)} m/s',
                        style: TextStyle(fontSize: 20),
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
          // Botones
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
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
          ),
        ],
      ),
    );
  }
}
