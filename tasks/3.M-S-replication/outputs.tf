output "app_instance_dns_name" {
  value       = aws_instance.socnet-app-instance.public_dns
  description = "App instance dns name"
}

output "dbmaster_instance_dns_name" {
  value       = aws_instance.socnet-dbmaster-instance.public_dns
  description = "Db master instance dns name"
}

output "dbslave1_instance_dns_name" {
  value       = aws_instance.socnet-dbslave-instance.public_dns
  description = "Db slave instance dns name"
}

output "dbslave2_instance_dns_name" {
  value       = aws_instance.socnet-dbslave2-instance.public_dns
  description = "Db slave instance dns name"
}

