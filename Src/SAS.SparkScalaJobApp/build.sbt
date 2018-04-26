name := "SAS.SparkScalaJobApp"

version := "1.0"

scalaVersion := "2.11.8"

assemblyJarName in assembly := "SAS.SparkScalaJobApp.jar"


libraryDependencies ++= Seq(
  "org.apache.spark" %% "spark-core" % "2.3.0" % "provided",
  "org.apache.spark" % "spark-sql_2.11" % "2.3.0" % "provided"
  //"org.apache.hadoop" %% "hadoop-core" % "1.2.1" % "provided"
)
        