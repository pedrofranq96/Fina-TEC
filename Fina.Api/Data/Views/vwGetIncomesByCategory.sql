CREATE OR ALTER VIEW [vwGetIncomesByCategory] AS
SELECT [Transaction].[UserId],
       [Category].[Title] AS [Category],
       YEAR([Transaction].[PaidOrReceivedAt]) AS [Year],
       SUM([Transaction].[Amount]) as [Incomes]
from [Transaction]
         inner join [Category]
                    on [Transaction].[CategoryId] = [Category].[Id]
WHERE
    [Transaction].[PaidOrReceivedAt] >= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
  AND [Transaction].[PaidOrReceivedAt] < DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
  AND [Transaction].[Type] = 1
GROUP BY
    [Transaction].[UserId],
    [Category].[Title],
    YEAR([Transaction].[PaidOrReceivedAt])
