extends RefCounted
class_name NationalEconomy

var national_budget: float = 100_000_000_000.0
var public_debt: float = 85_000_000_000.0
var gross_domestic_product: float = 420_000_000_000.0
var unemployment_rate: float = 7.4
var inflation_rate: float = 2.1
var daily_tax_revenue: float = 185_000_000.0
var daily_public_spending: float = 172_000_000.0

func advance_day() -> void:
	var balance: float = daily_tax_revenue - daily_public_spending
	national_budget += balance
	public_debt = maxf(0.0, public_debt - balance)
	gross_domestic_product *= 1.00008
	unemployment_rate = clampf(unemployment_rate - 0.002, 2.0, 30.0)
	inflation_rate = clampf(inflation_rate + randf_range(-0.015, 0.02), -1.0, 15.0)

func set_daily_finances(tax_revenue: float, spending: float) -> void:
	daily_tax_revenue = maxf(0.0, tax_revenue)
	daily_public_spending = maxf(0.0, spending)

func billions(value: float) -> String:
	return "%.1f Md €" % (value / 1_000_000_000.0)
